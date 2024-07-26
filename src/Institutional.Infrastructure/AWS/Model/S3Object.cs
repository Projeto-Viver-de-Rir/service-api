using System.IO;

namespace Institutional.Infrastructure.AWS.Model;

public class S3Object
{
    public string Name { get; set; } = null!;
    public MemoryStream InputStream { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}