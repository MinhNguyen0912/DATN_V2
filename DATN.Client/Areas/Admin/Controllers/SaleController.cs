using System.Text.RegularExpressions;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Models;
using DATN.Core.ViewModel.SaleProductVM;
using DATN.Core.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace DATN.Client.Areas.Admin.Controllers
{
	public class SaleController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private readonly ClientService _clientService;

		public SaleController(IUnitOfWork unitOfWork,ClientService clientService)
		{
			_unitOfWork = unitOfWork;
			_clientService = clientService;
		}

		[Area("Admin")]
        //[Authorize(Roles = "Admin")]
        [Route("Admin/[controller]/[action]")]
        public IActionResult Index()
        {
	        List<SaleProuductVM>  saleProuductVm = new List<SaleProuductVM>();
	        SaleProuductVM saleProuduct = new SaleProuductVM();
	        saleProuduct.TabName = "Tab 1";
	        saleProuduct.TabIndex = 1;
	        saleProuduct.DicountAmount = 0;
	        saleProuduct.ChangeMoney = 0;
	        saleProuduct.DicountAmount = 0;
	        saleProuduct.QuickCreateUserVM = new QuickCreateUserVM();
	        saleProuduct.ShoppingTabs = new List<ShoppingTab>();
	        saleProuduct.ProductCount = saleProuduct.ShoppingTabs.Count();
	        saleProuductVm.Add(saleProuduct);
	        var SaleProuductVMStored = JsonConvert.SerializeObject(saleProuductVm);
	        HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			return View(saleProuductVm);
		}

		[HttpGet]
		public async Task<IActionResult> GetProductByName(string name)
		{
			var response = await _clientService.GetList<Product_EAV>($"https://localhost:7095/api/ProductEAV/GetByName/{name}");
			return Json(new { data = response });
		}
		[HttpGet]
		public async Task<IActionResult> SearchUser(string search)
		{
			var response = await _clientService.GetList<AppUser>($"https://localhost:7095/api/User/SearchUserByInput/search?search={search}");
			return Json(new { data = response });
		}
		public bool IsValidVietnamesePhoneNumber(string phoneNumber)
		{
			
			string pattern = @"^(0[3|5|7|8|9])([0-9]{8})$";
    
			
			return Regex.IsMatch(phoneNumber, pattern);
		}
		[HttpPost]
		public async Task<IActionResult> AddNonExistingUserToTab(int Tab, string FullName)
		{

			try
			{
				var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
				var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
				QuickCreateUserVM quickCreateUserVm = new QuickCreateUserVM();
				quickCreateUserVm.UserId = new Guid();
				quickCreateUserVm.FullName = FullName;
				quickCreateUserVm.PhoneNumber = IsValidVietnamesePhoneNumber(FullName) == true ? FullName : "";
			
				var response = await _clientService.Post($"https://localhost:7095/api/User/QuickCreateUser/quick-create",quickCreateUserVm);
				var userCreated = JsonConvert.DeserializeObject<AppUser>(response);
				foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList())
				{
					saleProuductVm.QuickCreateUserVM.UserId = userCreated.Id;
					saleProuductVm.QuickCreateUserVM.FullName = userCreated.FullName;
				}
				var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
				HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
				return Json(new { data =  SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList()});
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}
		[HttpPost]
		public async Task<IActionResult> AddUserToTab(int Tab, Guid UserId, string FullName)
		{
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList())
			{
				saleProuductVm.QuickCreateUserVM.UserId = UserId;
				saleProuductVm.QuickCreateUserVM.FullName = FullName;
			}
			var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
			HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			return Json(new { data =  SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList()});
		}
		[HttpGet]
		public async Task<IActionResult> CaculateChangeMoneny(float ChangeMoney, int Tab)
		{
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			var resonse =ChangeMoney- SaleProductDeserialized.FirstOrDefault(c => c.TabIndex == Tab)?.TotalAmount;
			return Json(new { data = resonse});
		}
		[HttpPost]
		public async Task<IActionResult> PaymentProcess(int Tab,float ChangeMoney)
		{
			
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			var paymentTab = SaleProductDeserialized.FirstOrDefault(c => c.TabIndex == Tab);
			if (paymentTab!=null)
			{
				if (paymentTab.TotalAmount<=0)
				{
					return Json(new { isSucccess=false, data = "Vui lòng thêm sản phẩm"});
				}

				if (string.IsNullOrEmpty(paymentTab.QuickCreateUserVM.FullName))
				{
					return Json(new {isSucccess=false, data = "Vui lòng thêm khách hàng vào hoá đơn"});
				}

				if (ChangeMoney == 0)
				{
					return Json(new {isSucccess=false, data = "Vui lòng nhập số tiền khách đưa"});
				}
				//var response= await _clientService.Post<object>($"https://localhost:7095/api/Invoice/CreatePaymentOffline",paymentTab);
				return Json(new {isSucccess=true, data = paymentTab});
			}
			return Json(new { data = ""});
		}
		[HttpPost]
		public async Task<IActionResult> AddNewTab()
		{
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			if (SaleProductDeserialized.Count() + 1 > 3)
			{
				return Json(new {isSucccess=false, data = "Bạn chỉ có thể thêm tối đa 3 Tab"});
			}
			SaleProuductVM saleProuductVm = new SaleProuductVM();
			saleProuductVm.TabIndex = SaleProductDeserialized.Max(c => c.TabIndex + 1);
			saleProuductVm.TabName = "Tab " +saleProuductVm.TabIndex;
			saleProuductVm.DicountAmount = 0;
			saleProuductVm.ChangeMoney = 0;
			saleProuductVm.QuickCreateUserVM = new QuickCreateUserVM();
			saleProuductVm.DicountAmount = 0;
			
			saleProuductVm.ShoppingTabs = new List<ShoppingTab>();
			saleProuductVm.ProductCount = saleProuductVm.ShoppingTabs.Count();
			SaleProductDeserialized.Add(saleProuductVm);
			var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
			HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			return Json(new {isSucccess=true,  data = "" });
		}
		
        [HttpPost]
		public async Task<IActionResult> AddProdToCurrentTab(int ProductId, int VariantId, int Quantity, float Price,int Tab,string ProductName,string Variant)
		{
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			
				foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab))
				{
					if (saleProuductVm.ShoppingTabs.Any(c => c.ProductId == ProductId && c.VariantId == VariantId))
					{
						foreach (var shoppingTab in saleProuductVm.ShoppingTabs.Where(c=>c.ProductId == ProductId && c.VariantId == VariantId))
						{
							if (shoppingTab.Quantity < 3)
							{
								shoppingTab.Quantity += 1;
								
							}
						}
					}
					else
					{
						ShoppingTab shoppingTab = new ShoppingTab();
						shoppingTab.ProductId = ProductId;
						shoppingTab.VariantId = VariantId;
						shoppingTab.ProductName = ProductName;
						shoppingTab.Variant = Variant;
						shoppingTab.Quantity = 1;
						shoppingTab.Price = Price;
						saleProuductVm.ShoppingTabs.Add(shoppingTab);
					}
					saleProuductVm.ProductCount = saleProuductVm.ShoppingTabs.Count();
					saleProuductVm.TotalAmount = saleProuductVm.ShoppingTabs.Sum(c => c.Quantity * c.Price);
				}
				var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
				
				HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			
			return Json(new { data = SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList() });
		} 
		[HttpPost]
		public async Task<IActionResult> UpdateQuantityProduct(int ProductId, int VariantId, int Quantity,int Tab)
		{
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			
				foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab))
				{
					
						foreach (var shoppingTab in saleProuductVm.ShoppingTabs.Where(c=>c.ProductId == ProductId && c.VariantId == VariantId))
						{
							shoppingTab.Quantity = Quantity;
						}
						saleProuductVm.ProductCount = saleProuductVm.ShoppingTabs.Count();
						saleProuductVm.TotalAmount = saleProuductVm.ShoppingTabs.Sum(c => c.Quantity * c.Price);

				}
				var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
				HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			
			return Json(new { data = SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList() });
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int ProductId, int VariantId,int Tab)
		{
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			
				foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList())
				{
						foreach (var shoppingTab in saleProuductVm.ShoppingTabs.Where(c=>c.ProductId == ProductId && c.VariantId == VariantId).ToList())
						{
							saleProuductVm.ShoppingTabs.Remove(shoppingTab);
						}
						saleProuductVm.ProductCount = saleProuductVm.ShoppingTabs.Count();
						saleProuductVm.TotalAmount = saleProuductVm.ShoppingTabs.Sum(c => c.Quantity * c.Price);

				} 
				
			var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
			HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			return Json(new { data = SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList() });
		}
		[HttpGet]
		public async Task<IActionResult> OpenCurrentTab(int Tab)
		{
			
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			return Json(new { data = SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList() });
		}
		[HttpPost]
		public async Task<IActionResult> SaveNote(int Tab,string Note)
		{
			var getSaleProductStored = HttpContext.Session.GetString("SaleProductStored");
			var SaleProductDeserialized = string.IsNullOrEmpty(getSaleProductStored) == false ? JsonConvert.DeserializeObject<List<SaleProuductVM>>(getSaleProductStored) : new List<SaleProuductVM>();
			foreach (var saleProuductVm in SaleProductDeserialized.Where(c=>c.TabIndex==Tab).ToList())
			{
				saleProuductVm.Note = Note;

			} 
			var SaleProuductVMStored = JsonConvert.SerializeObject(SaleProductDeserialized);
			HttpContext.Session.SetString("SaleProductStored", SaleProuductVMStored);
			return Json(new { data ="" });
		}
		
        
	}
}
