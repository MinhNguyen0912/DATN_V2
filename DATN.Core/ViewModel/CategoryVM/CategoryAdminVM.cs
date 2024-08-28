using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryAdminVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int Level { get; set; }
        public bool IsVisible { get; set; } = true;

    }
}
