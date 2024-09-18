using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.PendingCartVM;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModel.ProdutEAVVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DATN.Client.Controllers
{
    
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public ProductController(ClientService clientService, IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _clientService = clientService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var requestUrl = $"{ApiPaths.Product}/GetAll";

            var listProducts = await _clientService.Get<List<ProductVM_EAV>>(requestUrl);

            return View(listProducts);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestUrl = $"{ApiPaths.ProductEAV}/GetById/{id}";
            var product = _unitOfWork.ProductEAVRepository.GetByIdCustom((int)id);
            var productResponse = _mapper.Map<ProductVM_EAV>(product);
            
            CommentPaging commentPaging = new CommentPaging();
            commentPaging.CurrentPage = 0;
            commentPaging.PageSize = 2;
            commentPaging.ProductId = id;
            var productCommentResponse = await _clientService.Post<CommentOverviewVM>($"{ApiPaths.Comment}/comment-by-product-id", commentPaging);
            ViewBag.productCommentResponse = productCommentResponse;
            return View(productResponse);
        }
        [Authorize(Roles ="User")]
        public async Task<IActionResult> AddToCart(int selectedVariantId, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return Redirect("/Identity/Account/Login");

            }
            var response = await _clientService.Post<int>($"{ApiPaths.PendingCart}/AddToPendingCart", new AddToPendingCartVM() { VariantId = selectedVariantId, UserId = userId });
            return Redirect($"/Product/Edit?id={response}");

        }
        public async Task<IActionResult> ViewAllComment(int? id, int? pageIndex, int? ratingStar)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestUrl = $"{ApiPaths.ProductEAV}/GetById/{id}";
            var productResponse = await _clientService.Get<ProductVM_EAV>(requestUrl);
            CommentPaging commentPaging = new CommentPaging();
            commentPaging.CurrentPage = pageIndex.HasValue ? (int)pageIndex : 0;
            commentPaging.PageSize = 20;
            commentPaging.StarRating = ratingStar;
            commentPaging.ProductId = id;
            var productCommentResponse = await _clientService.Post<CommentOverviewVM>($"{ApiPaths.Comment}/comment-by-product-id", commentPaging);
            var productEditViewModel = new Product_EAV_VM
            {
                Product = productResponse,
                CommentOverviewVm = productCommentResponse
            };

            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");
            return View(productEditViewModel);
           
        }

        public async Task<IActionResult> Search(string query)
        {
            ViewBag.SearchQuery = query;
            return View();
        }
    }


}
