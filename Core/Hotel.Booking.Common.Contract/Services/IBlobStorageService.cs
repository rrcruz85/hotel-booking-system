
namespace Hotel.Booking.Common.Contract.Services
{
    public interface IBlobStorageService
    {
        Task<bool> ExistsAsync(string containerName);
        Task<bool> ExistsAsync(string containerName, string blobName);
        Task<string> UploadTextAsync(string containerName, string blobName, string contentType, string content);
        Task<string> UploadStreamAsync(string containerName, string blobName, string contentType, Stream content);
        Task<string> AcquireLeaseAsync(string containerName, string blobName, TimeSpan? leaseTime);
        Task ReleaseLeaseAsync(string containerName, string blobName, string leaseId);
        Task RenewLeaseAsync(string containerName, string blobName, string leaseId);
        Task<List<string>> GetBlobNames(string containerName, string directoryRelativePath = null);
        Task<bool> GetBlobContent(string containerName, string blobName, Stream outputStream);
        Task<bool> DeleteBlobByName(string containerName, string blobName);
    }
}
