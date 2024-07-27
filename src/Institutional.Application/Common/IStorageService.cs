using Institutional.Application.Common.Responses;
using System.IO;
using System.Threading.Tasks;

namespace Institutional.Application.Common;

public interface IStorageService
{
    Task<OperationResult> UploadFileAsync(string BucketName, string FileName, MemoryStream InputStream);
}