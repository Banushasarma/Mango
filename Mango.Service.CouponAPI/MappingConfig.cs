using AutoMapper;
using Mango.Service.CouponAPI.Models;
using Mango.Service.CouponAPI.Models.Dto;

namespace Mango.Service.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<CouponDto, Coupon>();
                    config.CreateMap<Coupon, CouponDto>();
                }
                );
            return mapperConfiguration;
        }
    }
}
