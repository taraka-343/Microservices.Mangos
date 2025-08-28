using AutoMapper;
using mangos.services.CoupanAPI.Models;
using mangos.services.CoupanAPI.Models.dto;

namespace mangos.services.CoupanAPI
{
    public class mappingConfig
    {
        public static MapperConfiguration registerMaps()
        {
            var mappingConf = new MapperConfiguration(config =>
            {
                //mapping of coupan values into coupanDto obj and vise versa 
                config.CreateMap<coupan, coupanDto>();
                config.CreateMap<coupanDto, coupan>();
            });
            return mappingConf;
        }
    }
}
