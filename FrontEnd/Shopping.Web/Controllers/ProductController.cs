using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class ProductController : Controller
    {

        #region Feilds
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion
        public async Task<ActionResult> Index()
        {
            List<ProductDto> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await productService.GetAllProductAync<ResponseDto>(accessToken);
            if (response is not null || response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        #region Create Product

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(AddProductDto addProductDto)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await productService.CreateProductAsync<ResponseDto>(addProductDto, accessToken);
                if (response is not null || response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(addProductDto);
        }

        [HttpGet]
        public async Task<ActionResult> ProductCreate()
        {
            return View();
        }
        
        #endregion

        #region  Edit Product

        [HttpGet]
        public async Task<ActionResult> EditProduct(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if(response is not null || response.IsSuccess)
            {
                EditProductDto productDto = JsonConvert.DeserializeObject<EditProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(EditProductDto editProductDto)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await productService.UpdateProductAsync<ResponseDto>(editProductDto, accessToken);
                if (response is not null || response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(editProductDto);
        }
        
        #endregion
    
        #region  Delete Product

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            if(productId != 0)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await productService.DeleteProductAsync<ResponseDto>(productId, accessToken);
                if(response.IsSuccess ) return RedirectToAction(nameof(Index));
            }
            return View();
        }


        #endregion
    
    }
}
