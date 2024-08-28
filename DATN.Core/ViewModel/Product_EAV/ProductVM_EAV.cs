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
        public ICollection<Variant> Variants { get; set; }
        public double Rating { get; set; } = 5;
        public int RateCount { get; set; } = 0;

        public string Description { get; set; }
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Origin? Origin { get; set; }
        public List<CategoryProductVM>? CategoryProducts { get; set; }
        public List<CommentVM>? Comments { get; set; }
        public List<ImageVM>? Images { get; set; }
        public List<ProductPromotion>? PromotionProducts { get; set; }

    }
}
