using DATN.Client.Constants;
using DATN.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ClientService _clientService;
        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            var formattedDate = date.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            var encodedDate = Uri.EscapeDataString(formattedDate);

            return View();
        }
    }
}
