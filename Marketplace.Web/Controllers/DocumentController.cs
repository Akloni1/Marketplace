using Marketplace.Web.Models;
using Marketplace.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Marketplace.Web.Controllers
{
    public class DocumentController : Controller
    {

        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        }

        public IActionResult DocumentIndex()
        {
            return View();
        }

        public async Task<IActionResult> DocumentDownload(string code)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
           
            var res = await _documentService.GetDocumentByCode<FileDataDto>(code, token);
            return File(res.content, res.contentType, res.fileName);
        }
    }
}
