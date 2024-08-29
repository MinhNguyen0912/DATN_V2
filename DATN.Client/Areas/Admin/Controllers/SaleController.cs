using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
	public class SaleController : Controller
	{
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [Route("Admin/[controller]/[action]")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
