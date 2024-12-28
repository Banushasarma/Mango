using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Mango.Service.ShoppingCartAPI.Utility
{
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        // Implement logic to handle authentication for backend API requests
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Add authentication logic here
            // For example, retrieve JWT token from HttpContext and add it to the Authorization header
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
