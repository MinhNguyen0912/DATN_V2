using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProdutEAVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public ProductsController(ClientService clientService, IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _clientService = clientService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(ProductPaging request)
        {
            ProductPaging partnerPaging = new ProductPaging();


            partnerPaging = await _clientService.Post<ProductPaging>("https://localhost:7095/api/ProductEAV/GetProductPaging", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            //https://localhost:7095/api/Brand/GetAll
            var brandRespon = await _clientService.Get<List<BrandVM>>($"{ApiPaths.Brand}/GetAll");


            //https://localhost:7095/api/Brand/GetAll
            var cateRespon = await _clientService.Get<List<CategoryVM>>($"{ApiPaths.Category}/GetAll");

            //https://localhost:7095/api/Origin/GetAll
            var originRespon = await _clientService.Get<List<OriginVM>>($"{ApiPaths.Origin}/GetAll");

            //list Attribute 
            //https://localhost:7095/api/AttributeEAV/GetAll
            var listAttributes = await _clientService.Get<List<AttributeVM_EAV>>($"{ApiPaths.AttributeEAV}/GetAll");

            ViewBag.listOrigin = new SelectList((System.Collections.IEnumerable)originRespon, "Id", "Name");
            ViewBag.listBrand = new SelectList((System.Collections.IEnumerable)brandRespon, "BrandId", "Name");
            ViewBag.listCate = new SelectList((System.Collections.IEnumerable)cateRespon, "Id", "Name");
            ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)listAttributes, "AttributeId", "AttributeName");


            return View();
        }
        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductEAVVM productVM)
        {
            //    //if (!ModelState.IsValid)
            //    //{
            //    //    return await ReturnToCreateViewWithErrors(productVM);
            //    //}

            //    CreateProductApi product = new CreateProductApi
            //    {
            //        Name = productVM.Name,
            //        Description = productVM.Description,
            //        CreateBy = Guid.NewGuid(),
            //        Status = productVM.Status,
            //        OriginId = 1,
            //        BrandId = productVM.BrandId
            //    };

            //    try
            //    {
            //        //https://localhost:7095/api/Product/Create
            //        var productRequestUrl = $"{ApiPaths.Product}/Create";
            //        var productResponse = await _clientService.Post<ProductVM>(productRequestUrl, product);
            //        if (productResponse == null)
            //        {
            //            throw new Exception("Failed to create product.");
            //        }

            //        var variants = JsonConvert.DeserializeObject<List<ProductAttributeVM>>(productVM.VariantsJson);
            //        if (variants != null)
            //        {
            //            foreach (var variant in variants)
            //            {
            //                variant.ProductId = productResponse.Id;
            //                await _clientService.Post<ProductAttributeVM>($"{ApiPaths.ProductAtribute}/Create", variant);
            //            }
            //        }

            //        var dynamicAttributes = JsonConvert.DeserializeObject<List<ProductAttributeVM>>(productVM.DynamicAttributesJson);
            //        if (dynamicAttributes != null)
            //        {
            //            foreach (var dynamic in dynamicAttributes)
            //            {
            //                dynamic.ProductId = productResponse.Id;
            //                await _clientService.Post<ProductAttributeVM>($"{ApiPaths.ProductAtribute}/Create", dynamic);
            //            }
            //        }

            //        if (productVM.DefaultImage != null)
            //        {
            //            string data;
            //            using (var memoryStream = new MemoryStream())
            //            {
            //                await productVM.DefaultImage.CopyToAsync(memoryStream);
            //                var arr = memoryStream.ToArray();
            //                data = Convert.ToBase64String(arr);
            //            }
            //            var formData = new MultipartFormDataContent();
            //            formData.Add(new StringContent(data), "image");


            //            var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
            //            var a = await result.Content.ReadAsStringAsync();
            //            JObject jsonObject = JObject.Parse(a);
            //            var link = jsonObject.SelectToken("data.url").ToString();

            //            var imageDefaultResponse = link;

            //            if (imageDefaultResponse != null)
            //            {
            //                var defaultImageVm = new ImageVM
            //                {
            //                    ImagePath = imageDefaultResponse,
            //                    IsDefault = true,
            //                    ProductId = productResponse.Id
            //                };

            //                //https://localhost:7095/api/Images/CreateImageProduct
            //                await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
            //            }
            //        }

            //        if (productVM.Files != null && productVM.Files.Count > 0)
            //        {
            //            foreach (var file in productVM.Files)
            //            {
            //                string data;
            //                using (var memoryStream = new MemoryStream())
            //                {
            //                    await file.CopyToAsync(memoryStream);
            //                    var arr = memoryStream.ToArray();
            //                    data = Convert.ToBase64String(arr);
            //                }
            //                var formData = new MultipartFormDataContent();
            //                formData.Add(new StringContent(data), "image");


            //                var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
            //                var a = await result.Content.ReadAsStringAsync();
            //                JObject jsonObject = JObject.Parse(a);
            //                var link = jsonObject.SelectToken("data.url").ToString();

            //                var imageDefaultResponse = link;

            //                if (imageDefaultResponse != null)
            //                {
            //                    var defaultImageVm = new ImageVM
            //                    {
            //                        ImagePath = imageDefaultResponse,
            //                        IsDefault = false,
            //                        ProductId = productResponse.Id
            //                    };

            //                    //https://localhost:7095/api/Images/CreateImageProduct
            //                    await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
            //                }
            //            }
            //        }

            return RedirectToAction(nameof(Index));
            //    }
            //    catch (Exception ex)
            //    {
            //        ToastHelper.ShowError(TempData, ex.Message);
            //        return await ReturnToCreateViewWithErrors(productVM);
            //    }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _clientService.Get<ProductVM_EAV>($"{ApiPaths.ProductEAV}/GetById/{id}");
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            //https://localhost:7095/api/Brand/GetAll
            var brandRespon = await _clientService.Get<List<BrandVM>>($"{ApiPaths.Brand}/GetAll");


            //https://localhost:7095/api/Brand/GetAll
            var cateRespon = await _clientService.Get<List<CategoryVM>>($"{ApiPaths.Category}/GetAll");

            //https://localhost:7095/api/Origin/GetAll
            var originRespon = await _clientService.Get<List<OriginVM>>($"{ApiPaths.Origin}/GetAll");

            //list Attribute 
            //https://localhost:7095/api/AttributeEAV/GetAll
            var listAttributes = await _clientService.Get<List<AttributeVM_EAV>>($"{ApiPaths.AttributeEAV}/GetAll");

            ViewBag.listOrigin = new SelectList((System.Collections.IEnumerable)originRespon, "Id", "Name");
            ViewBag.listBrand = new SelectList((System.Collections.IEnumerable)brandRespon, "BrandId", "Name");
            ViewBag.listCate = new SelectList((System.Collections.IEnumerable)cateRespon, "Id", "Name");
            ViewBag.listAttributes = new SelectList((System.Collections.IEnumerable)listAttributes, "AttributeId", "AttributeName");


            return View(product);
        }
        //private async Task<IActionResult> ReturnToCreateViewWithErrors(CreateProductVM productVM)
        //{
        //    ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
        //    {
        //        Text = v.ToString(),
        //        Value = ((int)v).ToString()
        //    }).ToList(), "Value", "Text");

        //    //https://localhost:7095/api/Origin/GetAll
        //    var originRespon = await _clientService.Get<List<OriginVM>>($"{ApiPaths.Origin}/GetAll");
        //    //https://localhost:7095/api/Brand/GetAll
        //    var brandRespon = await _clientService.Get<List<BrandVM>>($"{ApiPaths.Brand}/GetAll");

        //    ViewBag.listOrigin = new SelectList((System.Collections.IEnumerable)originRespon, "Id", "Name");
        //    ViewBag.listBrand = new SelectList((System.Collections.IEnumerable)brandRespon, "BrandId", "Name");
        //    return View(productVM);
        //}

        //private async Task<string> UploadImageAsync(IFormFile file)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(memoryStream);
        //        var arr = memoryStream.ToArray();
        //        var data = Convert.ToBase64String(arr);

        //        var formData = new MultipartFormDataContent();
        //        formData.Add(new StringContent(data), "image");

        //        var result = await _httpClient.PostAsync("https://api.imgbb.com/1/upload?key=618de95e498c5f4c920cb57d6ca7434d", formData);
        //        var responseContent = await result.Content.ReadAsStringAsync();
        //        var jsonObject = JObject.Parse(responseContent);
        //        var link = jsonObject.SelectToken("data.url")?.ToString();

        //        return link;
        //    }
        //}

        //    // GET: Admin/Products/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var requestUrl = $"{ApiPaths.Product}/GetById/{id}";
        //        var productResponse = await _clientService.Get<ProductVM>(requestUrl);

        //        // https://localhost:7095/api/EAV/GetProductAttributes/id?id=1
        //        var attributeRequestUrl = $"{ApiPaths.EAV}/GetProductAttributes/id?id={id}";
        //        var attributesResponse = await _clientService.Get<List<ProductAttributeDetailVM>>(attributeRequestUrl);

        //        // https://localhost:7095/api/EAV/GetListAttributes/id?id=1
        //        var listattributeRequestUrl = $"{ApiPaths.EAV}/GetListAttributes/id?id={id}";
        //        var listattributesResponse = await _clientService.Get<List<AttributesVM>>(listattributeRequestUrl);

        //        var productEditViewModel = new ProductEditViewModel
        //        {
        //            Id = productResponse.Id,
        //            Name = productResponse.Name,
        //            Description = productResponse.Description,
        //            imagePath = productResponse.imagePath,
        //            Images = productResponse.Images,
        //            Product = productResponse,
        //            Attributes = attributesResponse,
        //            listAttributes = listattributesResponse
        //        };

        //        //https://localhost:7095/api/Origin/GetAll
        //        var originRespon = await _clientService.Get<List<OriginVM>>($"{ApiPaths.Origin}/GetAll");
        //        //https://localhost:7095/api/Brand/GetAll
        //        var brandRespon = await _clientService.Get<List<BrandVM>>($"{ApiPaths.Brand}/GetAll");

        //        ViewBag.listOrigin = new SelectList((System.Collections.IEnumerable)originRespon, "Id", "Name");
        //        ViewBag.listBrand = new SelectList((System.Collections.IEnumerable)brandRespon, "BrandId", "Name");

        //        ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
        //        {
        //            Text = v.ToString(),
        //            Value = ((int)v).ToString()
        //        }).ToList(), "Value", "Text");

        //        return View(productEditViewModel);
        //    }

        //    // POST: Admin/Products/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, UpdateProductVM productVM, [FromForm] IFormFile? defaultImage, [FromForm] List<IFormFile>? files, [FromForm] List<int> ImageIds, [FromForm] List<int> DeletedImageIds)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        try
        //        {
        //            // Cập nhật thông tin sản phẩm
        //            UpdateProductApi newProduct = new UpdateProductApi
        //            {
        //                Id = id,
        //                Name = productVM.Name,
        //                Description = productVM.Description,
        //                CreateBy = Guid.NewGuid(),
        //                Status = productVM.Status,
        //                OriginId = productVM.OriginId,
        //                BrandId = productVM.BrandId
        //            };

        //            var response = await _clientService.Put<UpdateProductApi>($"{ApiPaths.Product}/Update", newProduct);


        //                if (defaultImage != null)
        //                {
        //                    string data;
        //                    using (var memoryStream = new MemoryStream())
        //                    {
        //                        await productVM.DefaultImage.CopyToAsync(memoryStream);
        //                        var arr = memoryStream.ToArray();
        //                        data = Convert.ToBase64String(arr);
        //                    }
        //                    var formData = new MultipartFormDataContent();
        //                    formData.Add(new StringContent(data), "image");


        //                    var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
        //                    var a = await result.Content.ReadAsStringAsync();
        //                    JObject jsonObject = JObject.Parse(a);
        //                    var link = jsonObject.SelectToken("data.url").ToString();

        //                    var imageDefaultResponse = link;

        //                    if (imageDefaultResponse != null)
        //                    {
        //                        var defaultImageVm = new ImageVM
        //                        {
        //                            ImagePath = imageDefaultResponse,
        //                            IsDefault = true,
        //                            ProductId = id
        //                        };

        //                        //https://localhost:7095/api/Images/CreateImageProduct
        //                        await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
        //                    }
        //                }

        //                if (files != null && files.Count > 0)
        //                {
        //                    foreach (var file in files)
        //                    {
        //                        string data;
        //                        using (var memoryStream = new MemoryStream())
        //                        {
        //                            await file.CopyToAsync(memoryStream);
        //                            var arr = memoryStream.ToArray();
        //                            data = Convert.ToBase64String(arr);
        //                        }
        //                        var formData = new MultipartFormDataContent();
        //                        formData.Add(new StringContent(data), "image");


        //                        var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
        //                        var a = await result.Content.ReadAsStringAsync();
        //                        JObject jsonObject = JObject.Parse(a);
        //                        var link = jsonObject.SelectToken("data.url").ToString();

        //                        var imageDefaultResponse = link;

        //                        if (imageDefaultResponse != null)
        //                        {
        //                            var defaultImageVm = new ImageVM
        //                            {
        //                                ImagePath = imageDefaultResponse,
        //                                IsDefault = false,
        //                                ProductId = id
        //                            };

        //                            //https://localhost:7095/api/Images/CreateImageProduct
        //                            await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
        //                        }
        //                    }
        //                }


        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception ex)
        //        {
        //            ToastHelper.ShowError(TempData, ex.Message);
        //            return await ReturnToUpdateViewWithErrors(productVM);
        //        }
        //    }

        //    // Phương thức phụ để tải hình ảnh lên
        //    private async Task<string> UploadImage(IFormFile imageFile)
        //    {
        //        string data;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await imageFile.CopyToAsync(memoryStream);
        //            var arr = memoryStream.ToArray();
        //            data = Convert.ToBase64String(arr);
        //        }

        //        var formData = new MultipartFormDataContent();
        //        formData.Add(new StringContent(data), "image");

        //        var result = await _httpClient.PostAsync("https://api.imgbb.com/1/upload?key=YOUR_API_KEY", formData);
        //        var a = await result.Content.ReadAsStringAsync();
        //        JObject jsonObject = JObject.Parse(a);
        //        var link = jsonObject.SelectToken("data.url").ToString();

        //        return link;
        //    }


        //    private async Task<IActionResult> ReturnToUpdateViewWithErrors(UpdateProductVM productVM)
        //    {
        //        ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(v => new SelectListItem
        //        {
        //            Text = v.ToString(),
        //            Value = ((int)v).ToString()
        //        }).ToList(), "Value", "Text");


        //        //https://localhost:7095/api/Origin/GetAll
        //        var originRespon = await _clientService.Get<List<OriginVM>>($"{ApiPaths.Origin}/GetAll");
        //        //https://localhost:7095/api/Brand/GetAll
        //        var brandRespon = await _clientService.Get<List<BrandVM>>($"{ApiPaths.Brand}/GetAll");

        //        ViewBag.listOrigin = new SelectList((System.Collections.IEnumerable)originRespon, "Id", "Name");
        //        ViewBag.listBrand = new SelectList((System.Collections.IEnumerable)brandRespon, "BrandId", "Name");

        //        return View(productVM);
        //    }

        //    [HttpDelete, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var requestUrl = $"{ApiPaths.Product}/Delete?id={id}";
        //        var response = await _clientService.Get(requestUrl);

        //        return RedirectToAction(nameof(Index));
        //        //if (response.Equ)
        //        //{
        //        //}
        //        //else
        //        //{
        //        //    // Log error or display error message
        //        //    return BadRequest("Could not delete product");
        //        //}
        //    }
    }
}
