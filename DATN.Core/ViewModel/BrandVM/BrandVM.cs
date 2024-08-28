using DATN.Core.ViewModel.ImagePath;

namespace DATN.Core.ViewModel.BrandVM
{
    public class BrandVM
    {

        public int BrandId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; } = true;
        public string StatusDisplay => Status == true ? "Đang hoạt động" : "Dừng hoạt động";
        public string? ImageUrl { get; set; }
        public ICollection<ImageVM>? Images { get; set; } = new List<ImageVM>(); // Navigation property for one-to-many relationship with Image
        public ICollection<Product_EAV.ProductVM_EAV>? ProductEAVs { get; set; }
    }
}
