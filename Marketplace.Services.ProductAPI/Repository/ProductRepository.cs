using Apache.Ignite.Core.Client;
using Apache.Ignite.Core;
using AutoMapper;
using Marketplace.Services.ProductAPI.DbContexts;
using Marketplace.Services.ProductAPI.Models;
using Marketplace.Services.ProductAPI.Models.Dto;
using Marketplace.Services.ProductAPI.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections.Generic;

namespace Marketplace.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly HttpClient _httpClient;

        public ProductRepository(ApplicationDbContext db, IMapper mapper, IDistributedCache cache, HttpClient httpClient)
        {
            _db = db;
            _mapper = mapper;
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);
            if (product.Id > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == productId);
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> res = null;
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
                var cacheClient = igniteClient.GetOrCreateCache<string, List<Product>>("Marketplace");

               
                if (cacheClient.TryGet("Products", out var order)){
                    keysReceived.Enqueue("Products");
                    res = order;
                }
                else
                {
                    var productList = await _db.Products.ToListAsync();
                    cacheClient.Put("Products", productList);
                    keysCreated.Enqueue("Products");
                    res = productList;
                }
               
                stopwatch.Stop();
                //смотрим сколько миллисекунд было затрачено на выполнение
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            return _mapper.Map<List<ProductDto>>(res);
        }

        public async Task<byte[]> GetHttpHomePage()
        {

            byte[] res = null;
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
                var cacheClient = igniteClient.GetOrCreateCache<string, byte[]>("Marketplace");

                if (cacheClient.TryGet("https://localhost:7025/home/render", out var content))
                {
                    keysReceived.Enqueue("https://localhost:7025/home/render");
                    res = content;
                }
                else
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7025/home/render");
                    content = await response.Content.ReadAsByteArrayAsync();
                    cacheClient.Put("https://localhost:7025/home/render", content);
                    keysCreated.Enqueue("https://localhost:7025/home/render");
                    res = content;
                }
                stopwatch.Stop();
                //смотрим сколько миллисекунд было затрачено на выполнение
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            return res;
        }
    }
}
