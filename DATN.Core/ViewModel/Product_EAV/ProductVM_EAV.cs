using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.ProductCommentVM;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class ProductVM_EAV
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        // Navigation property
        public List<VariantVM>? Variants { get; set; }
        public double Rating { get; set; } = 5;
        public int RateCount { get; set; } = 0;
        public string? ImagePath { get; set; }

        public string Description { get; set; }
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public BrandVM.BrandVM? Brand { get; set; }
        public OriginVM.OriginVM? Origin { get; set; }
        public List<CategoryProductVM>? CategoryProducts { get; set; }
        public List<CommentVM>? Comments { get; set; }
        public List<ImageVM>? Images { get; set; }
        public List<ProductPromotionVM.ProductPromotionVM>? PromotionProducts { get; set; }


    }
}
