using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Institutional.Infrastructure.AWS.Model;
using System;
using System.Threading.Tasks;
using AWSCredentials = Institutional.Infrastructure.AWS.Model.AWSCredentials;

namespace Institutional.Infrastructure.AWS;

public class StorageService : IStorageService
{
    private readonly BasicAWSCredentials _credentials;

    public StorageService(AWSCredentials credentials)
    {
        _credentials = new BasicAWSCredentials(credentials.AccessKey, credentials.SecretKey);
    }

    public async Task<S3ResponseDto> UploadFileAsync(S3Object obj)
    {
        var config = new AmazonS3Config() 
        {
            RegionEndpoint = RegionEndpoint.EUWest2
        };

        var response = new S3ResponseDto();
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = obj.InputStream,
                Key = obj.Name,
                BucketName = obj.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            using var client = new AmazonS3Client(_credentials, config);
            var transferUtility = new TransferUtility(client);
            
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 201;
            response.Message = $"{obj.Name} has been uploaded successfully";
        }
        catch(AmazonS3Exception s3Ex)
        {
            response.StatusCode = (int)s3Ex.StatusCode;
            response.Message = s3Ex.Message;
        }
        catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
}