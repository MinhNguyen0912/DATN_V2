using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModels.SendMailVM
{
    public class SendMailVM
    {
        public string? Email { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
