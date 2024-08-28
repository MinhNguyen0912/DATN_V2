using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int Level { get; set; }
        public bool IsVisible { get; set; }  // Mặc định là true, nghĩa là hiển thị
        public string IsVisibleDisplay => IsVisible == true ? "True" : "False";
        public bool IsOnList { get; set; } // Menu hiển thị ở bảng chọn cate
        public string IsOnlistDisplay => IsVisible == true ? "True" : "False";
        public string? ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
        public CategoryVM? ParentCategory { get; set; }
        public List<CategoryVM>? SubCategories { get; set; }
        public List<CategoryTranslationVM>? CategoryTranslations { get; set; }
        public List<Product_EAV.ProductVM_EAV>? Products { get; set; }
        public List<TimeRangeVM.CategoryTimeRangeVM>? Ranger { get; set; }
    }
}
