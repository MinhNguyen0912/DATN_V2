using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class CreateAttributeVariantVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Value { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsShow { get; set; }
        //public int AttributeId { get; set; }
    }
}
