using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.OriginVM
{
    public class OriginVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Description { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
