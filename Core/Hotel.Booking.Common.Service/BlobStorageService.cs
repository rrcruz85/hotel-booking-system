using Hotel.Booking.Common.Contract.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Hotel.Booking.Common.Service
{
    [ExcludeFromCodeCoverage]
    public class BlobStorageService : IBlobStorageService
    {
        private readonly CloudBlobClient _blobClient;
        private readonly IConfiguration _config;

        public BlobStorageService(IConfiguration config)
        {
            _config = config;
            var storageAccount = CloudStorageAccount.Parse(_config["BlobStorageConnectionString"]);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<bool> ExistsAsync(string containerName)
        {
            var container = await GetContainer(containerName);
            return await container.ExistsAsync();
        }

        public async Task<bool> ExistsAsync(string containerName, string blobName)
        {
            var blob = await GetBlobReference(containerName, blobName);
            return await blob.ExistsAsync();
        }

        public async Task<string> UploadTextAsync(string containerName, string blobName, string contentType, string content)
        {
            var blob = await GetBlobReference(containerName, blobName);
            blob.Properties.ContentType = contentType;
            byte[] svgXml = Encoding.UTF8.GetBytes(content);
            await blob.UploadFromByteArrayAsync(svgXml, 0, svgXml.Length);
            await blob.SetPropertiesAsync();
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<string> UploadStreamAsync(string containerName, string blobName, string contentType, Stream content)
        {
            var blob = await GetBlobReference(containerName, blobName);
            blob.Properties.ContentType = contentType;
            await blob.UploadFromStreamAsync(content);
            await blob.SetPropertiesAsync();
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<string> AcquireLeaseAsync(string containerName, string blobName, TimeSpan? leaseTime)
        {
            var blob = await GetBlobReference(containerName, blobName);
            return await blob.AcquireLeaseAsync(leaseTime);
        }

        public async Task ReleaseLeaseAsync(string containerName, string blobName, string leaseId)
        {
            var blob = await GetBlobReference(containerName, blobName);
            await blob.ReleaseLeaseAsync(new AccessCondition { LeaseId = leaseId });
        }

        public async Task RenewLeaseAsync(string containerName, string blobName, string leaseId)
        {
            var blob = await GetBlobReference(containerName, blobName);
            await blob.RenewLeaseAsync(new AccessCondition { LeaseId = leaseId });
        }

        private async Task<CloudBlobContainer> GetContainerAsync(string containerName)
        {
            var containerReference = _blobClient.GetContainerReference(containerName);
            await containerReference.CreateIfNotExistsAsync();
            return containerReference;
        }

        private async Task<CloudBlockBlob> GetBlobReferenceAsync(string containerName, string blobName)
        {
            var container = await GetContainerAsync(containerName);
            return container.GetBlockBlobReference(blobName);
        }

        private async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            var containerReference = _blobClient.GetContainerReference(containerName);
            await containerReference.CreateIfNotExistsAsync();
            return containerReference;
        }

        private async Task<CloudBlockBlob> GetBlobReference(string containerName, string blobName)
        {
            var container = await GetContainer(containerName);
            return container.GetBlockBlobReference(blobName);
        }

        public async Task<bool> GetBlobContent(string containerName, string blobName, Stream outputStream)
        {
            var blob = await GetBlobReferenceAsync(containerName, blobName);
            if (await blob.ExistsAsync())
            {
                await blob.DownloadToStreamAsync(outputStream);
                return true;
            }
            return false;
        }

        public async Task<List<string>> GetBlobNames(string containerName, string directoryRelativePath = null)
        {
            var blobNames = new List<string>();

            var container = await GetContainerAsync(containerName);

            BlobContinuationToken? blobContinuationToken = null;

            if (!string.IsNullOrEmpty(directoryRelativePath))
            {
                var directory = container.GetDirectoryReference(directoryRelativePath);
                do
                {
                    var resultSegment = await directory.ListBlobsSegmentedAsync(
                        useFlatBlobListing: true,
                        blobListingDetails: BlobListingDetails.None,
                        maxResults: null,
                        currentToken: blobContinuationToken,
                        options: null,
                        operationContext: null
                    );

                    blobContinuationToken = resultSegment?.ContinuationToken;

                    blobNames.AddRange(resultSegment?.Results.Select(b => b.Uri.Segments.Last()).ToList() ?? new List<string>());

                }
                while (blobContinuationToken != null);

                return blobNames;
            }

            do
            {
                var resultSegment = await container.ListBlobsSegmentedAsync(
                    prefix: null,
                    useFlatBlobListing: true,
                    blobListingDetails: BlobListingDetails.None,
                    maxResults: null,
                    currentToken: blobContinuationToken,
                    options: null,
                    operationContext: null
                );

                blobContinuationToken = resultSegment?.ContinuationToken;

                blobNames.AddRange(resultSegment?.Results.Select(b => b.Uri.Segments.Last()).ToList() ?? new List<string>());

            }
            while (blobContinuationToken != null);

            return blobNames;
        }

        public async Task<bool> DeleteBlobByName(string containerName, string blobName)
        {
            var blob = await GetBlobReferenceAsync(containerName, blobName);
            if (await blob.ExistsAsync())
            {
                return await blob.DeleteIfExistsAsync();
            }
            return false;
        }
    }
}