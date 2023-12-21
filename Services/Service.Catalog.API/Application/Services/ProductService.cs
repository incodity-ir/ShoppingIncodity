using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.API.Application.Contracts;
using Service.Catalog.API.Application.Dtos;
using Service.Catalog.API.Infrustructure.Persistence;
using Service.Catalog.API.Models;

namespace Service.Catalog.API.Application.Services
{
    public class ProductService : IProductService
    {
        #region Fields
        private SqlServerApplicationDB context;
        private IMapper mapper;
        public ProductService(SqlServerApplicationDB Context, IMapper Mapper)
        {
            context = Context;
            mapper = Mapper;
        }

        #endregion
        public async Task<ProductDto> CreateProduct(AddProductDto AddproductDto)
        {
            Product product = mapper.Map<Product>(AddproductDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;

        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            var product = await context.Products.FindAsync(ProductId);
            if (product is not null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            List<Product> products = await context.Products.ToListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int ProductId)
        {
            Product product = await context.Products.FindAsync(ProductId);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProduct(EditProductDto EditproductDto)
        {
            var product = mapper.Map<Product>(EditproductDto);
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);

        }

    }
}