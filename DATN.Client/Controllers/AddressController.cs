using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers
{
    [AllowAnonymous]
    public class AddressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
