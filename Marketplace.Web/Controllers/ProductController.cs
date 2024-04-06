using Marketplace.Web.Models;
using Marketplace.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Marketplace.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetAllProductsAsync<ResponseDto>(token);

            var listProductDto = res?.Result != null && res.IsSuccess ?
                JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(res.Result.ToString()) :
                new List<ProductDto>();

            return View(listProductDto);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.CreateProductAsync<ResponseDto>(productDto, token);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(id, token);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var res = await _productService.UpdateProductAsync<ResponseDto>(productDto, token);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(id, token);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDto>(model.Id, token);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            return View(model);
        }


    }
}
