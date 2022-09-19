using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Common.Contract.Services
{
    public interface IBlobStorageService
    {
        Task<bool> ExistsAsync(string containerName);
        Task<bool> ExistsAsync(string containerName, string blobName);
        Task<string> UploadTextAsync(string containerName, string blobName, string content);
        Task<string> UploadStreamAsync(string containerName, string blobName, Stream content);
        Task<string> AcquireLeaseAsync(string containerName, string blobName, TimeSpan? leaseTime);
        Task ReleaseLeaseAsync(string containerName, string blobName, string leaseId);
        Task RenewLeaseAsync(string containerName, string blobName, string leaseId);
        Task<List<string>> GetBlobNames(string containerName, string directoryRelativePath = null);
        Task<bool> GetBlobContent(string containerName, string blobName, Stream outputStream);
    }
}
