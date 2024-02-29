using Microsoft.AspNetCore.Mvc;
using ShappingCartApi.Application.Contracts;
using ShappingCartApi.Models.Dtos;

namespace ShappingCartApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        #region Feilds

        private readonly ICartRepository cartRepository;
        private readonly ResponseDto responseDto;
        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
            responseDto = new();
        }

        #endregion

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cart = await cartRepository.GetCartByUserId(userId);
                responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return responseDto;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await cartRepository.CreateOrUpdateCart(cartDto);
                responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return responseDto;
        }

        [HttpPut("Update")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await cartRepository.CreateOrUpdateCart(cartDto);
                responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return responseDto;
        }

        [HttpDelete("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool result = await cartRepository.RemoveFromCart(cartId);
                responseDto.Result = result;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return responseDto;
        }
    }
}
