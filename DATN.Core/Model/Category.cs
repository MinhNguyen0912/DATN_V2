using DATN.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Core.Model
{
    public class Category : BaseEntity
    {
        public int? ParentCategoryId { get; set; }

        public bool IsVisible { get; set; } = true; // Menu hiển thị trên navmenu
        public bool IsOnList { get; set; } = true; // Menu hiển thị ở bảng chọn cate
        [Required]
        public int Level { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<Product_EAV.Product_EAV>? Products { get; set; }
        public List<CategoryTimeRange>? CategoryTimeRanges { get; set; }

    }
}
