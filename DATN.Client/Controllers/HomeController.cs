using DATN.Client.Constants;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DATN.Client.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly ClientService _clientService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, ClientService clientService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7095/api/Category/GetAll");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var cates = JsonConvert.DeserializeObject<List<CategoryVM>>(responseContent);
            ViewBag.Categories = cates ?? new List<CategoryVM>(); // Sử dụng partial view để điều hướng sang trang khác vẫn giữ được list category
            var promotions = await _clientService.Get<List<PromotionVM>>($"{ApiPaths.Promotion}/GetAllActive");
            ViewBag.Promotions = promotions ?? new List<PromotionVM>();
            return View(cates ?? new List<CategoryVM>());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult DanhGia()
        {
            return View();
        }
        public IActionResult DanhGiaChiTiet()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Chuyentrang()
        {
            return View();
        }
    }
}
