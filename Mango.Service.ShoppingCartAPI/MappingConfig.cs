using AutoMapper; 
using Mango.Service.ShoppingCartAPI.Models;
using Mango.Service.ShoppingCartAPI.Models.Dto;

namespace Mango.Service.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
                    config.CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
                }
                );
            return mapperConfiguration;
        }
    }
}
