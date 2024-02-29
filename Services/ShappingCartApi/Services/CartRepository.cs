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
            try
            {
                var cartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(p => p.UserId == userId);
                if (cartHeader is null) throw new ArgumentNullException(nameof(cartHeader));
                dbContext.CartDetails.RemoveRange(dbContext.CartDetails.Where(p => p.CartHeaderId == cartHeader.Id));
                dbContext.CartHeaders.Remove(cartHeader);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

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

            var cartheaderFromDb = await dbContext.CartHeaders.FirstOrDefaultAsync(p => p.UserId == cart.CartHeader.UserId);
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
            Cart cart = new()
            {
                CartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(p=>p.UserId == userId)
            };
            cart.CartDetails = dbContext.CartDetails.Where(p => p.CartHeaderId == cart.CartHeader.Id)
                .Include(p => p.Product);
            return mapper.Map<CartDto>(cart );
        }

        public async Task<bool> RemoveFromCart(int cartDetailId)
        {
            CartDetail cartDetail = await dbContext.CartDetails.FirstOrDefaultAsync(p => p.Id == cartDetailId);
            if (cartDetail is null) throw new ArgumentNullException(nameof(cartDetail));

            int totalCountCartItems = dbContext.CartDetails.Where(p => p.CartHeaderId == cartDetail.CartHeaderId).Count();
            if (totalCountCartItems == 1)
            {
                var cartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(p => p.Id == cartDetail.CartHeaderId);
                dbContext.CartHeaders.Remove(cartHeader);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return true;
        }
    }
}
