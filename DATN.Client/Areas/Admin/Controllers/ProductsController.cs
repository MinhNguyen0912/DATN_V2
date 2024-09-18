using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProdutEAVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IISIntegration;
using Newtonsoft.Json.Linq;


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
			// Tạo đối tượng ProductPaging để lưu kết quả
			ProductPaging productPaging = new ProductPaging();

			// Gửi yêu cầu tới API và nhận dữ liệu
			productPaging = await _clientService.Post<ProductPaging>("https://localhost:7095/api/ProductEAV/GetProductPaging", request);

			// Kiểm tra nếu không có dữ liệu trả về
			if (productPaging == null)
			{
				return NotFound();
			}

			// Sắp xếp danh sách sản phẩm theo ProductId (giảm dần)
			productPaging.Items = productPaging.Items.OrderByDescending(p => p.ProductId).ToList();

			// Trả dữ liệu về View
			return View(productPaging);
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
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    // Tách cateIds từ chuỗi và chuyển thành danh sách số nguyên
                    List<int> cateIdList = productVM.cateIds.Split(',').Select(int.Parse).ToList();

                    // Tạo đối tượng Product_EAV
                    Product_EAV product = new Product_EAV
                    {
                        ProductName = productVM.ProductName,
                        Description = productVM.Description,
                        Status = productVM.Status,
                        OriginId = productVM.OriginId,
                        BrandId = productVM.BrandId
                    };

                    var resultProduct = _unitOfWork.ProductEAVRepository.Create(product);
                    _unitOfWork.SaveChanges();
                    if (resultProduct == null)
                    {
                        throw new Exception("Failed to create product.");
                    }

                    var productId = product.ProductId;
                    // Kiểm tra và tạo CategoryProducts
                    foreach (var cateId in cateIdList)
                    {
                        // Kiểm tra xem ProductId và CategoryId đã tồn tại chưa
                        //var existingCategoryProduct = _unitOfWork.CategoryProductRepository
                        //    .Find(cp => cp.ProductId == productId && cp.CategoryId == cateId);

                        // Nếu chưa tồn tại thì thêm mới
                        //if (existingCategoryProduct == null)
                        //{
                            CategoryProduct categoryProduct = new CategoryProduct
                            {
                                ProductId = productId,
                                CategoryId = cateId
                            };
                            var categoryProductResult = _unitOfWork.CategoryProductRepository.Create(categoryProduct);
                            if (categoryProductResult == null)
                            {
                                throw new Exception("Failed to create category product.");
                            }
                        //}
                    }

                    // Tạo Variants và VariantAttributes
                    if (productVM.Variants != null)
                    {
                        foreach (var item in productVM.Variants)
                        {
                            // Tạo Variant
                            Variant variant = new Variant
                            {
                                ProductId = productId,
                                VariantName = item.Name,
                                Quantity = item.Quantity,
                                PuscharPrice = item.PuscharPrice,
                                SalePrice = item.SalelPrice,
                                AfterDiscountPrice = item.AfterDiscountPrice,
                                MaximumQuantityPerOrder = item.MaximumQuantityPerOrder,
                                Weight = item.Weight,
                                IsDefault = item.IsDefault,
                                IsActive = item.IsActive
                            };

                            var variantResult = _unitOfWork.VariantRepository.Create(variant);
                            _unitOfWork.SaveChanges();
                            if (variantResult == null)
                            {
                                throw new Exception("Failed to create variant.");
                            }
                            var variantId = variant.VariantId;
                            // Tách attributeValueIds và tạo VariantAttributes
                            var attributeValueIds = item.attributeValueIds.Split('/').Select(int.Parse).ToList();
                            foreach (var attributeValueId in attributeValueIds)
                            {
                                VariantAttribute variantAttribute = new VariantAttribute
                                {
                                    VariantId = variantId,
                                    AttributeValueId = attributeValueId
                                };
                                var variantAttributeResult = _unitOfWork.VariantAttributeRepository.Create(variantAttribute);
                                _unitOfWork.SaveChanges();
                                if (variantAttributeResult == null)
                                {
                                    throw new Exception("Failed to create variant attribute.");
                                }
                            }

                            // Tạo Specifications cho từng Variant
                            if (item.Specifications != null)
                            {
                                foreach (var spec in item.Specifications)
                                {
                                    Specification specification = new Specification
                                    {
                                        VariantId = variantId,
                                        Key = spec.Name,
                                        Value = spec.Value
                                    };
                                    var specResult = _unitOfWork.SpecificationRepository.Create(specification);
                                    _unitOfWork.SaveChanges();
                                    if (specResult == null)
                                    {
                                        throw new Exception("Failed to create specification.");
                                    }
                                }
                            }
                        }
                    }

                    // Xử lý upload ảnh mặc định
                    if (productVM.ImagesDefault != null)
                    {
                        var imageDefaultResponse = await UploadImage(productVM.ImagesDefault);
                        if (imageDefaultResponse == null)
                        {
                            throw new Exception("Failed to upload default image.");
                        }

                        Image defaultImageVm = new Image
                        {
                            ImagePath = imageDefaultResponse,
                            IsDefault = true,
                            ProductId = productId
                        };
                        //await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
                        var ImageResult = _unitOfWork.imageReponsiroty.Create(defaultImageVm);
                        _unitOfWork.SaveChanges();
                        if (ImageResult == null)
                        {
                            throw new Exception("Failed to create specification.");
                        }
                    }

                    // Xử lý upload ảnh bổ sung
                    if (productVM.Images != null && productVM.Images.Count > 0)
                    {
                        foreach (var file in productVM.Images)
                        {
                            var imageResponse = await UploadImage(file);
                            if (imageResponse == null)
                            {
                                throw new Exception("Failed to upload additional image.");
                            }

                            Image additionalImageVm = new Image
                            {
                                ImagePath = imageResponse,
                                IsDefault = false,
                                ProductId = productId
                            };
                            //await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", additionalImageVm);

                            var ImageResult = _unitOfWork.imageReponsiroty.Create(additionalImageVm);
                            _unitOfWork.SaveChanges();
                            if (ImageResult == null)
                            {
                                throw new Exception("Failed to create specification.");
                            }
                        }
                    }

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    ToastHelper.ShowSuccess(TempData, "Product created successfully!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    transaction.Rollback();
                    ToastHelper.ShowError(TempData, $"Error: {ex.Message}");
                    return await ReturnToCreateViewWithErrors(productVM);
                }
            }
        }


        private async Task<string> UploadImage(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageData = Convert.ToBase64String(memoryStream.ToArray());

                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(imageData), "image");

                var result = await _httpClient.PostAsync("https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
                if (result.IsSuccessStatusCode)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(responseContent);
                    return jsonObject.SelectToken("data.url")?.ToString();
                }
            }
            return null;
        }

        private async Task<IActionResult> ReturnToCreateViewWithErrors(CreateProductEAVVM productVM)
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
            return View(productVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = _unitOfWork.ProductEAVRepository.GetByIdCustom(id);
            var result = new UpdateProductVM
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = product.Status,
                OriginId = product.OriginId,
                BrandId = product.BrandId,
                Variants = product.Variants?.Select(v => new UpdateVariantVM
                {
                    VariantId = v.VariantId,
                    Name = v.VariantName,
                    Quantity = v.Quantity,
                    PuscharPrice = v.PuscharPrice,
                    SalelPrice = v.SalePrice,
                    AfterDiscountPrice = v.AfterDiscountPrice,
                    IsDefault = v.IsDefault,
                    MaximumQuantityPerOrder = v.MaximumQuantityPerOrder,
                    Weight = v.Weight,
                    IsActive = v.IsActive,
                    Specifications = v.Specifications?.Select(s => new UpdateSpecificationVM
                    {
                        Id = s.Id,
                        Key = s.Key,
                        Value = s.Value
                    }).ToList() ?? new List<UpdateSpecificationVM>()
                }).ToList() ?? new List<UpdateVariantVM>()
            };
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


            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductVM productVM)
        {

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                                   
                    var product = _unitOfWork.ProductEAVRepository.GetByIdCustom(productVM.ProductId);
                    // Tạo đối tượng Product_EAV

                    product.ProductName = productVM.ProductName;
                    product.Description = productVM.Description;
                    product.Status = productVM.Status;
                    product.OriginId = productVM.OriginId;
                    product.BrandId = productVM.BrandId;


                    _unitOfWork.ProductEAVRepository.Update(product);
                    _unitOfWork.SaveChanges();

                    var productId = product.ProductId;
                    List<int> cateIdList = new List<int>();
                    // Tách cateIds từ chuỗi và chuyển thành danh sách số nguyên
                    if (productVM.cateIds != null)
                    {
                        cateIdList = productVM.cateIds.Split(',').Select(int.Parse).ToList();
                        // Kiểm tra và tạo CategoryProducts
                        foreach (var cateId in cateIdList)
                        {
                            // Kiểm tra xem ProductId và CategoryId đã tồn tại chưa
                            var existingCategoryProduct = _unitOfWork.CategoryProductRepository
                                .Find(cp => cp.ProductId == productId && cp.CategoryId == cateId);

                            // Nếu chưa tồn tại thì thêm mới
                            if (existingCategoryProduct == null)
                            {
                                CategoryProduct categoryProduct = new CategoryProduct
                                {
                                    ProductId = productId,
                                    CategoryId = cateId
                                };
                                var categoryProductResult = _unitOfWork.CategoryProductRepository.Create(categoryProduct);
                                if (categoryProductResult == null)
                                {
                                    throw new Exception("Failed to create category product.");
                                }
                            }
                        }
                    }
                    // Tạo Variants và VariantAttributes
                    if (productVM.Variants != null)
                    {
                        
                        foreach (var item in productVM.Variants)
                        {
                            Variant variant;
                            if (item.VariantId != 0)
                            {
                                variant = _unitOfWork.VariantRepository.GetByIdCustom(item.VariantId);
                                variant.VariantName = item.Name;
                                variant.Quantity = item.Quantity;
                                variant.PuscharPrice = item.PuscharPrice;
                                variant.SalePrice = item.SalelPrice;
                                variant.AfterDiscountPrice = item.AfterDiscountPrice;
                                variant.IsDefault = item.IsDefault;
                                variant.MaximumQuantityPerOrder = item.MaximumQuantityPerOrder;
                                variant.IsActive = item.IsActive;
                                variant.Weight = item.Weight;
                                _unitOfWork.VariantRepository.Update(variant);
                            }
                            else
                            {
                                 variant = new Variant
                                {
                                    ProductId = productId,
                                    VariantName = item.Name,
                                    Quantity = item.Quantity,
                                    PuscharPrice = item.PuscharPrice,
                                    SalePrice = item.SalelPrice,
                                    AfterDiscountPrice = item.AfterDiscountPrice,
                                    IsDefault = item.IsDefault,
                                    IsActive = true,
                                    MaximumQuantityPerOrder = item.MaximumQuantityPerOrder,
                                    Weight = item.Weight,
                                };

                               _unitOfWork.VariantRepository.Create(variant);
                                _unitOfWork.SaveChanges();
                                // Tách attributeValueIds và tạo VariantAttributes
                                var attributeValueIds = item.attributeValueIds.Split('/').Select(int.Parse).ToList();
                                foreach (var attributeValueId in attributeValueIds)
                                {
                                    VariantAttribute variantAttribute = new VariantAttribute
                                    {
                                        VariantId = variant.VariantId,
                                        AttributeValueId = attributeValueId
                                    };
                                    var variantAttributeResult = _unitOfWork.VariantAttributeRepository.Create(variantAttribute);
                                    _unitOfWork.SaveChanges();
                                    if (variantAttributeResult == null)
                                    {
                                        throw new Exception("Failed to create variant attribute.");
                                    }
                                }
                            }
                            // Tạo Variant
                            
                            _unitOfWork.SaveChanges();


                            var variantid = variant.VariantId;
                            // tạo specifications cho từng variant
                            if (item.Specifications != null)
                            {
                                foreach (var spec in item.Specifications)
                                {
                                    Specification specification;
                                    if (spec.Id!=0)
                                    {
                                        specification =await _unitOfWork.SpecificationRepository.GetById(spec.Id);
                                        specification.Key = spec.Key;
                                        specification.Value = spec.Value;
                                        _unitOfWork.SpecificationRepository.Update(specification);
                                    }
                                    else
                                    {
                                        specification = new Specification
                                        {
                                            VariantId = variantid,
                                            Key = spec.Key,
                                            Value = spec.Value
                                        };
                                        var specresult = _unitOfWork.SpecificationRepository.Create(specification);
                                        _unitOfWork.SaveChanges();
                                        if (specresult == null)
                                        {
                                            throw new Exception("failed to create specification.");
                                        }
                                    }
                                }
                                _unitOfWork.SaveChanges();
                            }
                        }
                    }

                    // Xử lý upload ảnh mặc định
                    if (productVM.ImagesDefault != null)
                    {
                        var imageDefaultResponse = await UploadImage(productVM.ImagesDefault);
                        if (imageDefaultResponse == null)
                        {
                            throw new Exception("Failed to upload default image.");
                        }

                        ImageVM defaultImageVm = new ImageVM
                        {
                            ImagePath = imageDefaultResponse,
                            IsDefault = true,
                            ProductId = productId
                        };
                        await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", defaultImageVm);
                    }

                    // Xử lý upload ảnh bổ sung
                    if (productVM.Images != null && productVM.Images.Count > 0)
                    {
                        foreach (var file in productVM.Images)
                        {
                            var imageResponse = await UploadImage(file);
                            if (imageResponse == null)
                            {
                                throw new Exception("Failed to upload additional image.");
                            }

                            ImageVM additionalImageVm = new ImageVM
                            {
                                ImagePath = imageResponse,
                                IsDefault = false,
                                ProductId = productId
                            };
                            await _clientService.Post<ImageVM>($"{ApiPaths.Images}/CreateImageProduct", additionalImageVm);
                        }
                    }

                    // Commit transaction nếu mọi thứ thành công
                    transaction.Commit();
                    ToastHelper.ShowSuccess(TempData, "Product created successfully!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    transaction.Rollback();
                    ToastHelper.ShowError(TempData, $"Error: {ex.Message}");
                    return RedirectToAction(nameof(Index));
                }
            }
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
