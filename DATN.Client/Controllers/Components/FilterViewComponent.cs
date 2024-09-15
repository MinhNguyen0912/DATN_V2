using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers.Components
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FilterViewComponent> _logger;
        public FilterViewComponent(HttpClient httpClient, ILogger<FilterViewComponent> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IViewComponentResult> InvokeAsync(int cateId, int parrentCateId)
        {
            ViewBag.cateId = cateId;
            ViewBag.parrentCateId = parrentCateId;
            return View();
        }
    }
}
