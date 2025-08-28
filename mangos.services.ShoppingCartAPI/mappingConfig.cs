using AutoMapper;
using mangos.services.ShoppingCartAPI.Models;
using mangos.services.ShoppingCartAPI.Models.Dto;

namespace mangos.services.ShoppingCartAPI
{
    public class mappingConfig
    {
        public static MapperConfiguration registerMaps()
        {
            var mappingConf = new MapperConfiguration(config =>
            {
                //mapping of coupan values into coupanDto obj and vise versa 
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                //config.CreateMap<CartDto, Cart>().ReverseMap(); // if Cart model exists
            });
            return mappingConf;
        }
    }
}
