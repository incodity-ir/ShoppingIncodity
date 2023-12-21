using AutoMapper;
using Service.Catalog.API.Application.Dtos;
using Service.Catalog.API.Models;

namespace Service.Catalog.API.Application.Mapper
{
    public class MapConfiguration
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfiguration = new MapperConfiguration(config=>
            {
                config.CreateMap<Product,ProductDto>().ReverseMap();
                //config.CreateMap<ProductDto,Product>();
                config.CreateMap<AddProductDto,Product>();
                config.CreateMap<EditProductDto,Product>();
            });

            return mappingConfiguration;
        }
    }
}