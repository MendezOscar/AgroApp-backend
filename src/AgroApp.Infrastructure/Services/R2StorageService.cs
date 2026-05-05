using Amazon.S3;
using Amazon.S3.Model;
using AgroApp.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AgroApp.Infrastructure.Services;

public class R2StorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _publicUrl;

    public R2StorageService(IConfiguration configuration)
    {
        var r2 = configuration.GetSection("R2Settings");
        _bucketName = r2["BucketName"]!;
        _publicUrl = r2["PublicUrl"]!;

        var config = new AmazonS3Config
        {
            ServiceURL = r2["Endpoint"]!,
            ForcePathStyle = true,
            SignatureVersion = "4",
            AuthenticationRegion = "auto",
        };

        _s3Client = new AmazonS3Client(
            r2["AccessKeyId"],
            r2["SecretAccessKey"],
            config);
    }

    public async Task<string> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var key = $"crops/{DateTime.UtcNow:yyyy/MM/dd}/{Guid.NewGuid()}_{fileName}";

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = fileStream,
            ContentType = contentType,
            AutoCloseStream = false,
            UseChunkEncoding = false  // ← también aquí por seguridad
        };

        await _s3Client.PutObjectAsync(request, cancellationToken);
        return key;
    }

    public async Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = storageKey
        };

        await _s3Client.DeleteObjectAsync(request, cancellationToken);
    }

    public string GetPublicUrl(string storageKey)
        => $"{_publicUrl}/{storageKey}";
}