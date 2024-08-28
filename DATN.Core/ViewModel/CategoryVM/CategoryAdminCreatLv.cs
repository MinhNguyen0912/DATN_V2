using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryAdminCreatLv
    {

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public int Level { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsOnList { get; set; } = true;
        public int? ParentCategoryId { get; set; }
        public List<int>? RangerId { get; set; } = new List<int>();


    }
}
