

using AutoMapper;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Models;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.InvoiceDetailVM;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModel.PromotionVM;
using DATN.Core.ViewModel.TimeRangeVM;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.UserViewModel;

namespace DATN.Core.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<Contact, ContactVM>().ReverseMap();
                //config.CreateMap<Product, ProductVM>()
                //    .ForMember(dest => dest.imagePath, opt => opt.MapFrom(src => src.Images.FirstOrDefault(img => img.IsDefault == true).ImagePath))
                //    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                //    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                //    .ForMember(dest => dest.DefaultPrice, opt => opt.MapFrom(src => src.ProductAttributes.FirstOrDefault(p=>p.IsDefault==true).AfterDiscountPrice))
                //    .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => ((1 - ((src.ProductAttributes.FirstOrDefault(p => p.IsDefault == true).AfterDiscountPrice) / (src.ProductAttributes.FirstOrDefault(p => p.IsDefault == true).SalePrice))) * 100)))
                //    //.ForMember(dest => dest.ProductPromotionVM, opt => opt.MapFrom(src => src.PromotionProducts))
                //    .ReverseMap();
                //config.CreateMap<Attributes,AttributesVM>().ReverseMap();
                //config.CreateMap<CreateAttributeVM,AttributesVM>().ReverseMap();
                //config.CreateMap<AttributeValue, AttributeValueVM>().ReverseMap();
                //config.CreateMap<AttributeValue, CreateVLVariantVM>().ReverseMap();
                //config.CreateMap<AttributeValue, CreateValueVM>().ReverseMap();
                //config.CreateMap<AttributeValue, CreateAttributeVariantVM>().ReverseMap();
                //config.CreateMap<ProductAttribute, ProductAttributeVM>().ReverseMap();
                //config.CreateMap<ProductAttribute, CreateProductAttributeVM>().ReverseMap();


                config.CreateMap<Brand, BrandVM>().ReverseMap();
                config.CreateMap<Brand, BrandCreatVM>().ReverseMap();
                // config.CreateMap<Language, LanguageVM>().ReverseMap();
                config.CreateMap<Category, CategoryVM>().ReverseMap();
                config.CreateMap<Origin, OriginVM>().ReverseMap();
                config.CreateMap<AppUser, UserExportVM>().ReverseMap();
                config.CreateMap<AppUser, UserVM>().ReverseMap();
                config.CreateMap<Promotion, PromotionVM>().ReverseMap();
                config.CreateMap<Image, ImageVM>().ReverseMap();
                config.CreateMap<Image, CreateImageProductVM>().ReverseMap();

                config.CreateMap<Brand, BrandVM>().ReverseMap();
                config.CreateMap<CategoryProduct, CategoryProductVM>().ReverseMap();
                config.CreateMap<Voucher, VoucherVM>().ReverseMap();
                config.CreateMap<Invoice, InvoiceVM>().ReverseMap();
                config.CreateMap<InvoiceDetail, InvoiceDetailVM>()
                    .ReverseMap();
                config.CreateMap<Comment, CommentVM>().ReverseMap();
                config.CreateMap<TimeRange, TimeRangeVM>().ReverseMap();
                config.CreateMap<CategoryTimeRange, CategoryTimeRangeVM>().ReverseMap();
                config.CreateMap<Magazine, MagazineVM>().ReverseMap();
                config.CreateMap<Category, CategoryAdminVM>().ReverseMap();
                config.CreateMap<Category, CategoryAdminCreatLv>().ReverseMap();
                config.CreateMap<Voucher, VoucherVM>().ReverseMap();
                config.CreateMap<InvoiceDetail, InvoiceDetailForCommentVM>().ReverseMap();
                config.CreateMap<Invoice, InvoiceShowForClientVM>().ReverseMap();


                config.CreateMap<Attribute_EAV, AttributeVM_EAV>().ReverseMap();
                config.CreateMap<AttributeValue_EAV, AttributeValueVM_EAV>().ReverseMap();
                config.CreateMap<Product_EAV, ProductVM_EAV>().ReverseMap();
                config.CreateMap<Variant, VariantVM>().ReverseMap();
                config.CreateMap<VariantAttribute, VariantAttributeVM>().ReverseMap();

                config.AddProfile<AutoMapperConfig>();
            });
            return mapperConfig.CreateMapper();
        }
    }
}
