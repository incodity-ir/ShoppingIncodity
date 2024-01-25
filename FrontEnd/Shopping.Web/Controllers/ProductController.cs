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

        #region Get All Product
        public async Task<ActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await productService.GetAllProductAync<ResponseDto>();
            if (response is not null || response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        #endregion

        #region  Create New Product

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(AddProductDto addProductDto)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProductAsync<ResponseDto>(addProductDto);
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



        #region Update Product

        [HttpGet]
        public async Task<ActionResult> EditProduct(int productId)
        {

            var response = await productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response is not null || response.IsSuccess)
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
                var response = await productService.UpdateProductAsync<ResponseDto>(editProductDto);
                if (response is not null || response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(editProductDto);
        }

        #endregion

        #region Delete Product 

        [HttpGet]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var response = await productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response is not null || response.IsSuccess)
            {
                DeleteProductDto productDto = JsonConvert.DeserializeObject<DeleteProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> DeleteProduct(DeleteProductDto deleteProductDto)
        {
            if (deleteProductDto.ProductId != 0)
            {
                var response = await productService.DeleteProductAsync<ResponseDto>(deleteProductDto.ProductId);
                if (response is not null || response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(deleteProductDto);
        }

        #endregion

    }
}
