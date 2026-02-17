using Microsoft.Azure.Cosmos;
using DocVault.Api.Models;

namespace DocVault.Api.Services
{
    public class CosmosService
    {
        private readonly Container _container;

public CosmosService(IConfiguration configuration)
{
    // These come from your .env
    var endpoint = configuration["COSMOS_DB_ENDPOINT"];
    var key = configuration["COSMOS_DB_KEY"];
    
    // These come from your appsettings.json (Notice the colon : for nesting)
    var databaseName = configuration["CosmosDb:DatabaseName"]; 
    var containerName = configuration["CosmosDb:ContainerName"];

    var client = new CosmosClient(endpoint, key);
    _container = client.GetContainer(databaseName, containerName);
}

        public async Task AddDocumentAsync(DocumentMetadata metadata)
        {
            // Ensure the partition key (UserId) is set
            await _container.CreateItemAsync(metadata, new PartitionKey(metadata.UserId));
        }
    }
}