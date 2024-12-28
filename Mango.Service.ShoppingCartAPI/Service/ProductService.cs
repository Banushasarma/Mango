using Mango.Service.ShoppingCartAPI.Models.Dto;
using Mango.Service.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Service.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(products.Result.ToString());
            }
            else
            {
                return new List<ProductDto>();
            }

        }
    }
}
