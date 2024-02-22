using ShappingCartApi.Models.Dtos;

namespace ShappingCartApi.Application.Contracts
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> CreateOrUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailId);
        Task<bool> ClearCart(string userId);
    }
}
