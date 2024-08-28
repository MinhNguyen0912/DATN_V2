using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateVLVariantVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Value { get; set; }
        public int AttributeId { get; set; }
    }
}
