using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateDynamicProductVM
    {
        [Required(ErrorMessage = "Thuộc tính không được để trống.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá trị không được để trống.")]
        public string Value { get; set; }
    }
}
