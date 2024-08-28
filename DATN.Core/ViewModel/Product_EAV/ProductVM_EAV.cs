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

        public ICollection<CategoryProductVM>? CategoryProducts { get; set; }
        public ICollection<CommentVM>? Comments { get; set; }
        public ICollection<ImageVM>? Images { get; set; }
        public ICollection<ProductPromotion>? PromotionProducts { get; set; }
    }
}
