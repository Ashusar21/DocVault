using Newtonsoft.Json;

namespace DocVault.Api.Models
{
    public class DocumentMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string BlobUrl { get; set; } = string.Empty;

        public long Size { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
