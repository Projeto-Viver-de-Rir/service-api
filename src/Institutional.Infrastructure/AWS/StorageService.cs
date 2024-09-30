using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Threading.Tasks;
using AWSCredentials = Institutional.Infrastructure.AWS.Model.AWSCredentials;

namespace Institutional.Infrastructure.AWS;

public class StorageService : IStorageService
{
    private readonly BasicAWSCredentials _credentials;
    private readonly IMemoryCache _cache;

    public StorageService(AWSCredentials credentials, IMemoryCache cache)
    {
        _credentials = new BasicAWSCredentials(credentials.AccessKey, credentials.SecretKey);
        _cache = cache;
    }

    public async Task<OperationResult> UploadFileAsync(string BucketName, string FileName, MemoryStream InputStream)
    {
        var config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.SAEast1
        };
        
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                CannedACL = S3CannedACL.NoACL,
                BucketName = BucketName,
                InputStream = InputStream,
                Key = FileName
            };

            using var client = new AmazonS3Client(_credentials, config);
            var transferUtility = new TransferUtility(client);
            
            await transferUtility.UploadAsync(uploadRequest);

            return new OperationResult{ StatusCode = 201, Message = $"{FileName} has been uploaded successfully." };
        }
        catch(AmazonS3Exception s3Ex)
        {
            return new OperationResult{ StatusCode = (int)s3Ex.StatusCode, Message = s3Ex.Message };
        }
        catch(Exception ex)
        {
            return new OperationResult{ StatusCode = 500, Message = ex.Message };
        }
    }
    
    public async Task<string?> GetFilePathAsync(string FileName, string BucketName = "institutional-app")
    {
        var maximumExpiration = DateTime.UtcNow.AddDays(30);
        
        var filePath = _cache.GetOrCreateAsync(FileName, async item =>
        {
            item.SlidingExpiration = TimeSpan.FromDays(10);
            item.AbsoluteExpiration = maximumExpiration;
            
            try
            {
                using var client = new AmazonS3Client(_credentials,
                    new AmazonS3Config { RegionEndpoint = RegionEndpoint.SAEast1 });

                return await client.GetPreSignedURLAsync(new GetPreSignedUrlRequest()
                {
                    BucketName = BucketName, Key = FileName, Expires = maximumExpiration
                });
            }
            catch (Exception)
            {
                return null;
            }
        });

        return await filePath;
    }
}