using DATN.Core.Enum;

namespace DATN.Core.ViewModel.MagazineVM
{
    public class MagazineVM
    {
        public int MagazineId { get; set; }

        public string? Image { get; set; }
        public string Caption { get; set; }
        public MagazineStatus Status { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
