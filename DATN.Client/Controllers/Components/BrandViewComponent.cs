using DATN.Core.ViewModel.Product_EAV;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Controllers.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductsViewComponent> _logger;

        public BrandViewComponent(HttpClient httpClient, ILogger<ProductsViewComponent> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        private async Task<List<ProductVM_EAV>> GetProductsByBrand(string? branId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7095/api/Product/GetProductByBrand?brandId={branId}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductVM_EAV>>(responseContent);

            return products ?? new List<ProductVM_EAV>();
        }
        public async Task<IViewComponentResult> InvokeAsync(string? brandId)
        {
            var products = new List<ProductVM_EAV>();
            if (brandId != null)
            {
                products = await GetProductsByBrand(brandId);
            }
            return View(products);



        }

    }
}
