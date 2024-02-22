using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShappingCartApi.Application.Contracts;
using ShappingCartApi.Models;
using ShappingCartApi.Models.Dtos;
using ShappingCartApi.Persistence;

namespace ShappingCartApi.Services
{
    public class CartRepository : ICartRepository
    {
        #region Fileds

        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CartRepository(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #endregion
        public async Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> CreateOrUpdateCart(CartDto cartDto)
        {
            Cart cart = mapper.Map<Cart>(cartDto);
            var prod =await  dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId);
            if (prod == null)
            {
                dbContext.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await dbContext.SaveChangesAsync();
            }

            var cartheaderFromDb = dbContext.CartHeaders.FirstOrDefaultAsync(p => p.UserId == cart.CartHeader.UserId);
            if (cartheaderFromDb == null)
            {
                dbContext.CartHeaders.Add(cart.CartHeader);
                await dbContext.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                dbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await dbContext.CartDetails.FirstOrDefaultAsync(p =>
                    p.ProductId == cart.CartDetails.FirstOrDefault().ProductId && p.CartHeaderId == cart.CartHeader.Id);
                if (cartDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    dbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    //cart.CartDetails.FirstOrDefault().Count= cart.CartDetails.FirstOrDefault().Count + cartDetail.Count;

                    //refactoring
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    dbContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    dbContext.SaveChangesAsync();
                }
            }

            return mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
