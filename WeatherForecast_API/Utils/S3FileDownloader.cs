using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast_API.Utils
{
    public class S3FileDownloader
    {
        /// <summary>
        /// downloads object async from AWS S3 to local machine
        /// </summary>
        /// <param name="fileKey">file path in s3 bucket</param>
        /// <param name="storedFile">where to store the file in local machine</param>
        /// <param name="region">bucket region</param>
        /// <param name="accessKey">bucket accesKey (empty string if it's a public bukcet)</param>
        /// <param name="secretKey">bucket secretKey (empty string if it's a public bucket)</param>
        /// <exception cref="AmazonS3Exception"></exception>
        public static async Task DownloadFileAsync(string bucket, string fileKey, string storedFile, RegionEndpoint region,string accessKey,string secretKey)
        {
            BasicAWSCredentials creds = new BasicAWSCredentials(accessKey,secretKey);
            AmazonS3Client s3Client = new AmazonS3Client(creds, region);
            TransferUtility fileTransferUtility = new TransferUtility(s3Client);
            await fileTransferUtility.DownloadAsync(storedFile, bucket, fileKey);
        }
    }
}
