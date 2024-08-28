using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class LanguageVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Name { get; set; }
        //public string Description { get; set; } = string.Empty;
        //public DateTime CreateAt { get; set; } = DateTime.Now;
        //public DateTime? DeleteAt { get; set; }
        //public DateTime? UpdateAt { get; set; }
        //public Guid? CreateBy { get; set; }
        //public Guid? UpdateBy { get; set; }
        //public Guid? DeleteBy { get; set; }
    }
}
