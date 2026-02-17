using Azure.Storage.Blobs;

namespace DocVault.Api.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

public BlobService(IConfiguration configuration)
{
    var connectionString = configuration["BLOB_STORAGE_CONNECTION_STRING"];
    var containerName = configuration["BlobStorage:ContainerName"];

    var blobServiceClient = new BlobServiceClient(connectionString);
    _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

    // FIX: You must ensure the container exists before uploading.
    // Note: In a constructor, we use the synchronous version:
    _containerClient.CreateIfNotExists(); 
}

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobClient = _containerClient.GetBlobClient($"{Guid.NewGuid()}-{file.FileName}");

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }
    }
}
