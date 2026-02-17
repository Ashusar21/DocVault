using Microsoft.AspNetCore.Mvc;

namespace DocVault.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Documents endpoint working!");
        }
    }
}
