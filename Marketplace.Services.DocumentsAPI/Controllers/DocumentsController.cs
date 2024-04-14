using Marketplace.Services.DocumentsAPI.Models;
using Marketplace.Services.DocumentsAPI.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace Marketplace.Services.DocumentsAPI.Controllers
{
    [ApiController]
    [Route("api/document")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public DocumentsController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{code}")]
        public async Task<FileData> GetDocumentDocxByCode(string code)
        {

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),
            };



            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();

            var dirClone = Path.GetFullPath(Path.Combine(code));
            
            var contentCache = await _cache.GetAsync("content");

            byte[] content;
            if (contentCache is null)
            {
                Console.WriteLine("Не Из Кэша");
                content = await System.IO.File.ReadAllBytesAsync(dirClone);
                await _cache.SetAsync("content", content, options);
            }
            else
            {
                Console.WriteLine("Из Кэша");
                content = contentCache;
            }
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            var fileName = Path.GetFileName(dirClone);
            new FileExtensionContentTypeProvider()
                .TryGetContentType(fileName, out string contentType);

            return new FileData { content = content, contentType = contentType, fileName = fileName };
        }
    }
}
