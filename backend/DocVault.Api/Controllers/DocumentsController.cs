using Microsoft.AspNetCore.Mvc;
using DocVault.Api.Services;
using DocVault.Api.Models;

namespace DocVault.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly BlobService _blobService;
        private readonly CosmosService _cosmosService;

        public DocumentsController(BlobService blobService, CosmosService cosmosService)
        {
            _blobService = blobService;
            _cosmosService = cosmosService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("File is required.");

            if (string.IsNullOrWhiteSpace(request.UserId))
                return BadRequest("UserId is required.");

            var blobUrl = await _blobService.UploadFileAsync(request.File);

            var metadata = new DocumentMetadata
            {
                UserId = request.UserId,
                FileName = request.File.FileName,
                BlobUrl = blobUrl,
                Size = request.File.Length
            };

            await _cosmosService.AddDocumentAsync(metadata);

            return Ok(metadata);
        }
    }

    // DTO to handle Swagger Form Data mapping
    public class DocumentUploadRequest
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }
    }
}