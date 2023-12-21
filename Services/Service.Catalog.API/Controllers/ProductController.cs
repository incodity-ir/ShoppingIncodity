using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.API;
using Service.Catalog.API.Application.Contracts;
using Service.Catalog.API.Application.Dtos;

namespace MyApp.Namespace
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ResponseDto responseDto;
        private IProductService productService;
        public ProductController(IProductService ProductService)
        {
            productService = ProductService;
            responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var productDtos = await productService.GetAllProduct();
                responseDto.Result = productDtos;
            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>{ex.Message};
            }

            return responseDto;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetById(int Id)
        {
            try
            {
                ProductDto product = await productService.GetProductById(Id);
                responseDto.Result = product;
            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>{ex.Message};
            }

            return responseDto;
        }

        [HttpPost]
        public async Task<object> AddProduct([FromBody] AddProductDto AddproductDto)
        {
            try
            {
                ProductDto product = await productService.CreateProduct(AddproductDto);
                responseDto.Result = product;
            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>{ex.Message};
            }

            return responseDto;
        }

        [HttpPut]
        public async Task<object> UpdateProduct([FromBody] EditProductDto editProductDto)
        {
                        try
            {
                ProductDto product = await productService.UpdateProduct(editProductDto);
                responseDto.Result = product;
            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>{ex.Message};
            }

            return responseDto;
        }

        [HttpDelete]
        public async Task<object> DeleteProduct(int ProductId)
        {
            try
            {
                bool Sucess = await productService.DeleteProduct(ProductId);
                responseDto.DisplayMessage ="Product deleted successfully";
            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>{ex.Message};
            }
            return responseDto;
        }
    }
}
