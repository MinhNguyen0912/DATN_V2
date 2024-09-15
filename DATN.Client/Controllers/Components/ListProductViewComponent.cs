using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.ViewModel.ListProductCompVM;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers.Components
{
    [Authorize(Roles = "User")]
    public class ListProductViewComponent : ViewComponent
    {
        private readonly ClientService _clientService;
        public ListProductViewComponent(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int promotionId)
        {
            var listProductCompVM = new ListProductCompVM();

            var listProductCompVMs = new List<ListProductCompVM>();
            var promotion = await _clientService.Get<PromotionVM>($"{ApiPaths.Promotion}/GetById?promotionId={promotionId}");

            if (promotion != null)
            {
                listProductCompVM.BannerUrl = promotion.BannerUrl;
                listProductCompVM.BackgroundColor = promotion.BackgroundColor;
                listProductCompVM.Percent = promotion.Percent;
                var products = await _clientService.Get<List<ProductVM_EAV>>($"{ApiPaths.ProductEAV}/GetProductByPromotion?promotionId={promotion.Id}");
                if (products!=null)
                {
                    foreach (var item in products)
                    {
                        var productRating = await _clientService.Get<double>($"{ApiPaths.Product}/GetProductRating?productId={item.ProductId}");
                        var productRateCount = await _clientService.Get<int>($"{ApiPaths.Product}/GetProductRateCount?productId={item.ProductId}");
                        item.Rating = productRating;
                        item.RateCount = productRateCount;
                                            }
                    listProductCompVM.Products = products;
                }
            }
            ViewBag.PromotionId = promotionId;

            return View(listProductCompVM);
        }
    }
}
