using AutoMapper;
using Marketplace.Services.ProductAPI.DbContexts;
using Marketplace.Services.ProductAPI.Models;
using Marketplace.Services.ProductAPI.Models.Dto;
using Marketplace.Services.ProductAPI.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

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
            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();
            var productList = await _cache.GetRecordAsync<List<Product>>("Products");

            if (productList is null)
            {
                productList = await _db.Products.ToListAsync();
                await _cache.SetRecordAsync("Products", productList);
            }
            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return _mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<byte[]> GetHttpHomePage()
        {
            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();
            byte[] content = await _cache.GetAsync("https://localhost:7025/home/render");

            if (content is null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                };
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7025/home/render");
                content = await response.Content.ReadAsByteArrayAsync();
                await _cache.SetAsync("https://localhost:7025/home/render", content, options);
            }

            stopwatch.Stop();
            //смотрим сколько миллисекунд было затрачено на выполнение
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return content;
        }
    }
}
