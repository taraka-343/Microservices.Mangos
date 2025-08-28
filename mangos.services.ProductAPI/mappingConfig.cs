using AutoMapper;
using mangos.services.ProductAPI.Models;
using mangos.services.ProductAPI.Models.Dto;

namespace mangos.services.ProductAPI
{
    public class mappingConfig
    {
        public static MapperConfiguration registerMaps()
        {
            var mappingConf = new MapperConfiguration(config =>
            {
                //mapping of coupan values into coupanDto obj and vise versa 
                config.CreateMap<product, productDto>();
                config.CreateMap<productDto, product>();
            });
            return mappingConf;
        }
    }
}
