using AutoMapper;
using Service.Catalog.API.Application.Dtos;
using Service.Catalog.API.Models;

namespace Service.Catalog.API.Application.Mapper
{
    
    public class ProductMap:Profile
    {
        public ProductMap()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<AddProductDto,Product>();
            CreateMap<EditProductDto,Product>();
        }
    }
    
}