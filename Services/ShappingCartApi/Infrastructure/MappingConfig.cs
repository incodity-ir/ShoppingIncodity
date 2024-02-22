using AutoMapper;
using ShappingCartApi.Models;
using ShappingCartApi.Models.Dtos;

namespace ShappingCartApi.Infrastructure
{
    public class MappingConfig
    {
        public static MapperConfiguration congigMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>()
                    .ForMember(m => m.ProductId, m => m.MapFrom(m => m.ProductId))
                    .ForMember(m => m.Name, m => m.MapFrom(m => m.Name))
                    .ForMember(m => m.Description, m => m.MapFrom(m => m.Description))
                    .ForMember(m => m.CategoryName, m => m.MapFrom(m => m.CategoryName))
                    .ForMember(m => m.Price, m => m.MapFrom(m => m.Price))
                    .ForMember(m => m. ImageURL, m => m.MapFrom(m => m.ImageURL)).ReverseMap();

                config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
