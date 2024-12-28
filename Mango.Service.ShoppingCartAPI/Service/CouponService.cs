using Mango.Service.ShoppingCartAPI.Models.Dto;
using Mango.Service.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Service.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CouponDto>(coupons.Result.ToString());
            }
            else
            {
                return new CouponDto();
            }
        }
    }
}
