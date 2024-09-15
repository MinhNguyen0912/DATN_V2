using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers
{
    [Authorize(Roles = "User")]
    public class AddressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
