using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;
using Marketplace.Services.DocumentsAPI.Models;
using Marketplace.Services.DocumentsAPI.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
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
            var keysCreated = new ConcurrentQueue<string>();
            var keysReceived = new ConcurrentQueue<string>();

            var clientConfiguration = new IgniteClientConfiguration
            {
                Endpoints = new List<string>
                    {
                    "localhost"
                    }
            };

            using (var igniteClient = Ignition.StartClient(clientConfiguration))
            {
                Stopwatch stopwatch = new Stopwatch();
                //засекаем время начала операции
                stopwatch.Start();

                var dirClone = Path.GetFullPath(Path.Combine(code));



                var cacheClient = igniteClient.GetOrCreateCache<string, byte[]>("Marketplace");

                if (cacheClient.TryGet("content", out var content))
                {
                    keysReceived.Enqueue("content");
                }
                else
                {
                    content = await System.IO.File.ReadAllBytesAsync(dirClone);
                    cacheClient.Put("content", content);
                    keysCreated.Enqueue("content");
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
}
