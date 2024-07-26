using Institutional.Infrastructure.AWS.Model;
using System.Threading.Tasks;

namespace Institutional.Infrastructure.AWS;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3Object obj);
}