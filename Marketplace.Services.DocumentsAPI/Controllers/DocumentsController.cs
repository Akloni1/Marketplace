using Marketplace.Services.DocumentsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Marketplace.Services.DocumentsAPI.Controllers
{
    [ApiController]
    [Route("api/document")]
    public class DocumentsController : ControllerBase
    {
        [HttpGet("{code}")]
        public async Task<FileData> GetDocumentDocxByCode(string code)
        {

            var dirClone = Path.GetFullPath(Path.Combine(code));

            var fileName = System.IO.Path.GetFileName(dirClone);
            var content = await System.IO.File.ReadAllBytesAsync(dirClone);
            new FileExtensionContentTypeProvider()
                .TryGetContentType(fileName, out string contentType);

            return new FileData { content = content, contentType = contentType, fileName = fileName};
           // return File(content, contentType, fileName);
        }
    }
}
