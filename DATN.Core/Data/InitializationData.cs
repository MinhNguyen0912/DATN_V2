using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Attribute_EAV = DATN.Core.Model.Product_EAV.Attribute_EAV;
using AttributeValue_EAV = DATN.Core.Model.Product_EAV.AttributeValue_EAV;
using Product_EAV = DATN.Core.Model.Product_EAV.Product_EAV;

namespace DATN.Core.Data
{
    public static class InitializationData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Promotion
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { Id = 1, Name = "Giảm giá 10%", Description = "Khuyến mãi giảm giá 10% cho tất cả các sản phẩm", Percent = 10, From = DateTime.Now, To = DateTime.Now.AddMonths(1), IsActive = false },
                new Promotion { Id = 2, Name = "Mua 1 tặng 1", Description = "Chương trình mua 1 tặng 1 cho sản phẩm đã chọn", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(2), IsActive = false },
                new Promotion { Id = 3, Name = "Miễn phí vận chuyển", Description = "Miễn phí vận chuyển cho đơn hàng trên 500,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(3), IsActive = false },
                new Promotion { Id = 4, Name = "Tặng voucher 100,000 VNĐ", Description = "Nhận voucher trị giá 100,000 VNĐ cho mỗi đơn hàng", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(4), IsActive = false },
                new Promotion { Id = 5, Name = "Giảm 50% cho sản phẩm mới", Description = "Khuyến mãi giảm giá 50% cho tất cả sản phẩm mới ra mắt", Percent = 50, From = DateTime.Now, To = DateTime.Now.AddMonths(5), IsActive = false },
                new Promotion { Id = 6, Name = "Tặng quà cho đơn hàng trên 1,000,000 VNĐ", Description = "Nhận quà tặng hấp dẫn khi đặt hàng trên 1,000,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(6), IsActive = false },
                new Promotion { Id = 7, Name = "Giảm 20% cho sản phẩm phụ kiện", Description = "Khuyến mãi giảm giá 20% cho tất cả sản phẩm phụ kiện", Percent = 20, From = DateTime.Now, To = DateTime.Now.AddMonths(7), IsActive = false },
                new Promotion { Id = 8, Name = "Đổi cũ lấy mới", Description = "Chương trình đổi sản phẩm cũ lấy sản phẩm mới với giá ưu đãi", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(8), IsActive = false },
                new Promotion { Id = 9, Name = "Mua hàng tặng phiếu mua hàng", Description = "Mua hàng tặng phiếu mua hàng trị giá 200,000 VNĐ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(9), IsActive = false },
                new Promotion { Id = 10, Name = "Đồng giảm giá 50%", Description = "Siêu sale đồng giảm 50% giá sản phẩm", Percent = 50, From = DateTime.Now, To = DateTime.Now.AddMonths(10), BannerUrl = "/Images/Component/BannerPromotion1.gif", IsActive = false },
                new Promotion { Id = 11, Name = "Sôi động cùng mùa Ơ-rô", Description = "Siêu giảm giá mùa Ơ-RÔ", Percent = 0, From = DateTime.Now, To = DateTime.Now.AddMonths(10), BannerUrl = "/Images/Component/BannerPromotion2.webp", IsActive = true }
            );
            #endregion
            #region Magazine
            modelBuilder.Entity<Magazine>().HasData(
                new Magazine { MagazineId = 1, Image = "/Images/Magazine/Picture1.jpg", Caption = "Danh sách trúng thưởng - Tưng bừng \"Tháng Panasonic\": Cơ hội trúng 102 bộ quà giá trị lên đến 655 triệu đồng", CreateAt = DateTime.Now.AddDays(1), Status = MagazineStatus.Starting, Content = "<h2 style=\"margin-left:0px;\">Cơn sốt game chưa bao giờ hạ nhiệt! Bạn muốn chiến game \"cực phê\" với trải nghiệm đỉnh cao? Chọn ngay <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh\">màn hình</a> chơi game ROG và nhận quà \"khủng\" - nâng cấp trải nghiệm, chiến thắng mọi thử thách!</h2><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:845/442;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/ROG.jpg\" alt=\"Chiến gam hay nhận quà đỉnh\" width=\"845\" height=\"442\"></p><h3 style=\"margin-left:0px;\">1. Thời gian khuyến mãi: Từ 13/08 đến 30/09/2024</h3><h3 style=\"margin-left:0px;\">2. Nội dung chương trình</h3><p style=\"margin-left:0px;\">Trong thời gian khuyến mãi, khi mua mua <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k\">Màn hình Asus Gaming ROG Strix XG27ACS</a> tại Thế giới Di động và Điện máy XANH, bạn sẽ nhận ngay 01 <a href=\"https://rog.asus.com/vn/mice-mouse-pads/mice/ambidextrous/rog-strix-impact-iii-model/\">Chuột ROG STRIX IMPACT III</a>.</p><p style=\"margin-left:0px;\">Áp dụng cho cả mua online và offline.</p><p style=\"margin-left:0px;\">Chương trình có thể kết thúc sớm nếu hết quà.</p><p style=\"margin-left:0px;\">Hạn chót đăng ký nhận quà: 03/10/2024.</p><h3 style=\"margin-left:0px;\">3. Sản phẩm áp dụng</h3><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img class=\"image_resized\" style=\"aspect-ratio:600/600;height:auto !important;width:150px;\" src=\"https://cdn.tgdd.vn/Products/Images/5697/325113/asus-xg27acs-27-inch-2k-thumb-600x600.jpg\" width=\"600\" height=\"600\"></a></p><figure class=\"image image_resized\" style=\"height:auto !important;width:40px !important;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img style=\"aspect-ratio:40/40;\" src=\"https://cdn.tgdd.vn/ValueIcons/label-baohanh3nam.png\" width=\"40\" height=\"40\"></a></figure><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><span style=\"color:rgb(51,51,51);\"><strong>Asus Gaming 27 inch 2K XG27ACS</strong></span></a></p><p style=\"margin-left:0px;\">Ngừng kinh doanh</p><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\">Xem chi tiết</a></p><h3 style=\"margin-left:0px;\">4. Cách thức nhận quà</h3><p style=\"margin-left:0px;\">Click vào nút \"Đăng ký\" trên website sự kiện <a href=\"https://www.asus.com/vn/events/infoM/activity_ROGLCD\">https://www.asus.com/vn/events/infoM/activity_ROGLCD</a>.</p><p style=\"margin-left:0px;\">- Đính kèm hình ảnh hóa đơn mua hàng, phiếu thu, phiếu xuất kho có dấu xác nhận của cửa hàng, thể hiện rõ tên sản phẩm và ngày mua hàng trong thời gian chương trình diễn ra.</p><p style=\"margin-left:0px;\">- Đính kèm ảnh chụp số S/N của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền serial của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền Họ và tên người nhận quà.</p><p style=\"margin-left:0px;\">- Điền Địa chỉ nhận quà.</p><p style=\"margin-left:0px;\">- Điền Số điện thoại liên hệ.</p><p style=\"margin-left:0px;\">Hình chụp số S/N trên sản phẩm phải chụp kèm với hoá đơn, chỉ chấp nhận hình chụp số S/N trên sản phẩm, số S/N trên hộp không có hiệu lực.</p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/786;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image1%20%283%29.jpg\" alt=\"Bước 1\" width=\"732\" height=\"786\"></p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/753;height:auto !important;width:100%;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image2%20%283%29.jpg\" alt=\"Bước 2\" width=\"732\" height=\"753\"></p><h3 style=\"margin-left:0px;\">4. Thông tin lưu ý</h3><p style=\"margin-left:0px;\">- Trong trường hợp không thể đăng ký trên hệ thống, quý khách hàng vui lòng gửi thông tin nhận quà như bên dưới về địa chỉ Email: dangkyasus@gmail.com với tiêu đề: \"[Khuyen mai] LCD GAMING\".</p><p style=\"margin-left:0px;\">- ASUS sẽ kiểm tra và liên hệ gửi quà tới bạn trong 20 ngày làm việc (trừ thứ 7 &amp; Chủ nhật).</p><p style=\"margin-left:0px;\">- Chương trình không áp dụng đồng thời với các chương trình khuyến mãi khác.</p><p style=\"margin-left:0px;\">- Quà tặng không có giá trị quy đổi thành tiền mặt hoặc các giá trị khác tương đương.</p><p style=\"margin-left:0px;\">- Hóa đơn/ phiếu thu/ phiếu xuất kho phải có con dấu của cửa hàng bán lẻ.</p><p style=\"margin-left:0px;\">- Chương trình chỉ dành cho khách hàng mua lẻ từ cửa hàng, tất cả hóa đơn từ nhà phân phối đều không được tham gia chương trình khuyến mãi này.</p><p style=\"margin-left:0px;\">- Dung lượng hình ảnh hóa đơn tải lên hệ thống &lt;1Mb.</p><p style=\"margin-left:0px;\">- Trong trường hợp cần thiết nhằm bảo đảm quyền lợi khách hàng, phía ASUS sẽ cần thêm những thông tin khác để xác định khách mua hàng là chính xác như: hóa đơn đỏ, giấy tờ tùy thân có ảnh đại diện,...</p><p style=\"margin-left:0px;\">Đây là chương trình của ASUS không phải của dienmayxanh.com và thegioididong.com, mọi góp ý hay thắc mắc khác về chương trình, xin vui lòng post tại <a href=\"https://www.facebook.com/share/g/NB6YhgtTY4wDwQ47/?mibextid=K35XfP\">Hội linh kiện PC ASUS ROG Việt Nam</a>.</p>" },
                new Magazine { MagazineId = 2, Image = "/Images/Magazine/Picture2.jpg", Caption = "Mua ngay máy lạnh Daikin: Thêm năm bảo hành, an tâm chất \"Nhật\"", CreateAt = DateTime.Now.AddDays(2), Status = MagazineStatus.Starting, Content = "Content 2" },
                new Magazine { MagazineId = 3, Image = "/Images/Magazine/Picture3.jpg", Caption = "Mua sớm máy lạnh - Tặng 2 lần vệ sinh chỉ có tại Điện máy XANH | Mới 2024", CreateAt = DateTime.Now.AddDays(3), Status = MagazineStatus.Starting, Content = "Content 3" },
                new Magazine { MagazineId = 4, Image = "/Images/Magazine/Picture4.jpg", Caption = "Khai lộc thăng hạng, vượt đỉnh thăng hoa: Mua Tivi LG nhận ưu đãi đến 51.000.000đ cùng gói ứng dụng giải trí hấp dẫn", CreateAt = DateTime.Now.AddDays(4), Status = MagazineStatus.Starting, Content = "Content 4" }
            );
            #endregion
            #region AppUser
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = Guid.Parse("2753c921-2304-4f8d-b8d5-75229d3b20d6"),
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Admin",
                    Dob = new DateTime(1990, 1, 1),
                    Address = "123 Main St, City A",
                    Description = "Admin",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                },
                new AppUser
                {
                    Id = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                    UserName = "customer@gmail.com",
                    NormalizedUserName = "CUSTOMER@GMAIL.COM",
                    Email = "customer@gmail.com",
                    NormalizedEmail = "CUSTOMER@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Customer",
                    Dob = new DateTime(1991, 2, 2),
                    Address = "456 Oak St, City B",
                    Description = "Customer",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                }
                , new AppUser
                {
                    Id = Guid.Parse("AA7C5218-4F1E-4AC6-A3B4-08DCB162E29E"),
                    UserName = "customer2@gmail.com",
                    NormalizedUserName = "CUSTOMER2@GMAIL.COM",
                    Email = "customer2@gmail.com",
                    NormalizedEmail = "CUSTOMER2@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    FullName = "Customer",
                    Dob = new DateTime(1991, 2, 2),
                    Address = "456 Oak St, City B",
                    Description = "Customer",
                    LastLoginTime = DateTime.UtcNow,
                    isActive = true
                }

            // Add more users as needed
            );
            #endregion
            #region Brand
            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "LG", Status = true, ImageUrl = "/Images/Brand/Lg.png" },
                new Brand { BrandId = 2, Name = "Samsung", Status = true, ImageUrl = "/Images/Brand/Samsung.png" },
                new Brand { BrandId = 3, Name = "Toshiba", Status = true, ImageUrl = "/Images/Brand/Toshiba.png" },
                new Brand { BrandId = 4, Name = "Sony", Status = true, ImageUrl = "/Images/Brand/Sony.png" },
                new Brand { BrandId = 5, Name = "Aqua", Status = true, ImageUrl = "/Images/Brand/Aqua.png" },
                new Brand { BrandId = 6, Name = "Xiaomi", Status = true, ImageUrl = "/Images/Brand/Xiaomi.png" }
            );
            #endregion
            #region Category
            modelBuilder.Entity<Category>().HasData(
                // Category level 0
                new Category { Id = 1, Name = "Hàng cao cấp", Level = 0, ImageUrl = "/Images/Home/Luxury.png", IsOnList = true },
                new Category { Id = 2, Name = "Tivi, Loa, dàn karaoke", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 3, Name = "Tủ lạnh, Tủ đông, Tủ mát", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 4, Name = "Máy giặt, Máy sấy quần áo", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 5, Name = "Máy lạnh, Máy nước nóng", Level = 0, ImageUrl = "/Images/Home/Hot.png", IsOnList = true },
                new Category { Id = 6, Name = "Điện gia dụng, Sinh tố, Xay ép", Level = 0, ImageUrl = "/Images/Home/Icons-1.png", IsOnList = true },
                new Category { Id = 7, Name = "Bếp điện, Nồi cơm, Đồ bếp", Level = 0, ImageUrl = "/Images/Home//Hot.png", IsOnList = true },
                new Category { Id = 8, Name = "Máy lọc nước, Nồi chiên", Level = 0, ImageUrl = "/Images/Home//Hot.png", IsOnList = true },
                new Category { Id = 9, Name = "Máy hút bụi, Máy rửa chén", Level = 0, ImageUrl = "/Images/Home/Icons-2.png", IsOnList = true },
                new Category { Id = 10, Name = "Xe đạp, Sức khỏe, Làm đẹp", Level = 0, ImageUrl = "/Images/Home/Icons-3.png", IsOnList = true },
                new Category { Id = 11, Name = "Đồ nghề - dụng cụ sửa chữa", Level = 0, ImageUrl = "/Images/Home/Icons-4.png", IsOnList = true },
                new Category { Id = 12, Name = "Điện thoại, Laptop, Tablet", Level = 0, ImageUrl = "/Images/Home/Icons-5.png", IsOnList = true },
                new Category { Id = 13, Name = "Phụ kiện, Camera, Đồng hồ", Level = 0, ImageUrl = "/Images/Home/Icons-6.png", IsOnList = true },
                new Category { Id = 14, Name = "Máy cũ, Dịch vụ hữu ích", Level = 0, ImageUrl = "/Images/Home/Icons-7.png", IsOnList = true },

                // Category level 1
                new Category { Id = 15, Name = "Tivi", ParentCategoryId = 2, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 16, Name = "Loa", ParentCategoryId = 2, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 17, Name = "Phụ kiện Tivi", ParentCategoryId = 2, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 18, Name = "Tủ lạnh", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 19, Name = "Tủ đông", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 20, Name = "Tủ mát", ParentCategoryId = 3, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 21, Name = "Máy giặt", ParentCategoryId = 4, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 22, Name = "Máy sấy quần áo", ParentCategoryId = 4, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 23, Name = "Phụ kiện máy giặt", ParentCategoryId = 4, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 24, Name = "Máy lạnh", ParentCategoryId = 5, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 25, Name = "Máy nước nóng", ParentCategoryId = 5, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 26, Name = "Máy xay các loại", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 27, Name = "Máy ép trái cây", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 28, Name = "Điện gia dụng", ParentCategoryId = 6, IsVisible = true, Level = 1, IsOnList = true },
                new Category { Id = 29, Name = "Nồi", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 30, Name = "Bếp", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                new Category { Id = 31, Name = "Lò", ParentCategoryId = 6, IsVisible = false, Level = 1, IsOnList = true },
                // Category level 2

                new Category { Id = 32, Name = "Màn hình cong", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 33, Name = "Màn hình phẳng", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 34, Name = "Siêu mỏng", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 35, Name = "Tivi cao cấp", ParentCategoryId = 15, Level = 2, IsOnList = true },
                new Category { Id = 36, Name = "Tivi thiết kế đặc biệt", ParentCategoryId = 15, Level = 2, IsOnList = false },
                new Category { Id = 37, Name = "Loa kéo", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 38, Name = "Loa kéo điện", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 39, Name = "Loa karaoke xách tay", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 40, Name = "Loa bluetooth", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 41, Name = "Loa thanh (SoundBar)", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 42, Name = "Dàn Karaoke, Amply", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 43, Name = "Dàn âm thanh", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 44, Name = "Micro", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 45, Name = "Loa thùng", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 46, Name = "Loa mini", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 47, Name = "Loa đồ chơi", ParentCategoryId = 16, Level = 2, IsOnList = true },
                new Category { Id = 48, Name = "Cáp HDMI", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 49, Name = "Khung treo Tivi", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 50, Name = "Điều khiển Tivi", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 51, Name = "Android TV Box", ParentCategoryId = 17, Level = 2, IsOnList = true },
                new Category { Id = 52, Name = "Dán màn Tivi", ParentCategoryId = 17, Level = 2, IsOnList = false },
                new Category { Id = 53, Name = "Sticker Tivi", ParentCategoryId = 17, Level = 2, IsOnList = false },
                new Category { Id = 54, Name = "Tủ lạnh 2 cánh", ParentCategoryId = 18, Level = 2, IsOnList = true }
                // Thêm các danh mục khác nếu cần
            );
            #endregion
            #region Origin
            modelBuilder.Entity<Origin>().HasData(
                new Origin { Id = 1, Name = "Trung Quốc", Description = "Xuất xứ: Trung Quốc", CreateAt = DateTime.Now },
                new Origin { Id = 2, Name = "Hàn Quốc", Description = "Xuất xứ: Hàn Quốc", CreateAt = DateTime.Now },
                new Origin { Id = 3, Name = "Mỹ", Description = "Xuất xứ: Mỹ", CreateAt = DateTime.Now },
                new Origin { Id = 4, Name = "Nhật Bản", Description = "Xuất xứ: Nhật Bản", CreateAt = DateTime.Now },
                new Origin { Id = 5, Name = "Đức", Description = "Xuất xứ: Đức", CreateAt = DateTime.Now },
                new Origin { Id = 6, Name = "Anh", Description = "Xuất xứ: Anh", CreateAt = DateTime.Now },
                new Origin { Id = 7, Name = "Pháp", Description = "Xuất xứ: Pháp", CreateAt = DateTime.Now },
                new Origin { Id = 8, Name = "Việt Nam", Description = "Xuất xứ: Việt Nam", CreateAt = DateTime.Now },
                new Origin { Id = 9, Name = "Úc", Description = "Xuất xứ: Úc", CreateAt = DateTime.Now },
                new Origin { Id = 10, Name = "Canada", Description = "Xuất xứ: Canada", CreateAt = DateTime.Now }
                // Add more origins as needed
            );
            #endregion
            #region Image
            modelBuilder.Entity<Image>().HasData(
                new Image() { ImageId = 1, ImagePath = "/Images/Component/ListProduct/product1.webp", IsDefault = true, ProductId = 1},
                new Image() { ImageId = 2, ImagePath = "/Images/Component/ListProduct/product2.webp", IsDefault = true, ProductId = 2},
                new Image() { ImageId = 3, ImagePath = "/Images/Component/ListProduct/product1-1.jpg", IsDefault = false, ProductId = 1},
                new Image() { ImageId = 4, ImagePath = "/Images/Component/ListProduct/product1-2.jpg", IsDefault = false, ProductId = 1},
                new Image() { ImageId = 5, ImagePath = "/Images/Component/ListProduct/product1-3.jpg", IsDefault = false, ProductId = 1},
                new Image() { ImageId = 6, ImagePath = "/Images/Component/ListProduct/product1-4.jpg", IsDefault = false, ProductId = 1},
                new Image() { ImageId = 7, ImagePath = "/Images/Component/ListProduct/product1-5.jpg", IsDefault = false, ProductId = 1}
            );
            #endregion
            #region CategoryProduct
            modelBuilder.Entity<CategoryProduct>().HasData(
                new CategoryProduct { CategoryProductId = 1, CategoryId = 33, ProductId = 1 },
                new CategoryProduct { CategoryProductId = 2, CategoryId = 35, ProductId = 1 },
                new CategoryProduct { CategoryProductId = 3, CategoryId = 54, ProductId = 2 }
                // Thêm dữ liệu mẫu khác nếu cần
            );
            #endregion
            #region InvoiceDetail
            //modelBuilder.Entity<InvoiceDetail>().HasData(
            //    new InvoiceDetail { InvoiceDetailId = 1, InvoiceId = 1, Quantity = 2, ProductAttributeId = 1, NewPrice = 10000000, PuscharPrice = 9354545 , OldPrice = 10290000 },
            //    new InvoiceDetail { InvoiceDetailId = 2, InvoiceId = 2, Quantity = 1, ProductAttributeId = 2, NewPrice = 11000000, PuscharPrice = 10809091, OldPrice= 11990000 },
            //    new InvoiceDetail { InvoiceDetailId = 3, InvoiceId = 3, Quantity = 1, ProductAttributeId = 3, NewPrice = 11500000, PuscharPrice = 10627273, OldPrice= 11690000 },
            //    new InvoiceDetail { InvoiceDetailId = 4, InvoiceId = 3, Quantity = 1, ProductAttributeId = 2, NewPrice = 11000000, PuscharPrice = 10809091, OldPrice = 11990000 }
            //);
            #endregion
            #region Comment
            //modelBuilder.Entity<Comment>().HasData(
            //    new Comment { CommentId = 1, Content = "Chất lượng khá tốt so với giá tiền", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Default, InvoiceDetailId = 1, Rating = 4, ProductId = 1 },
            //    new Comment { CommentId = 2, Content = "Sản phẩm tạm ổn", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Done, InvoiceDetailId = 2, Rating = 3, ProductId = 1 },
            //    new Comment { CommentId = 3, Content = "Chất lượng sản phẩm tuyệt vời", Date = DateTime.Now, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), Type = CommentType.Processing, Rating = 5, ProductId = 1, InvoiceDetailId = 3 }
            //);
            #endregion
            #region Invoice
            //modelBuilder.Entity<Invoice>().HasData(
            //    new Invoice { InvoiceId = 1, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), VoucherUserId=1 },
            //    new Invoice { InvoiceId = 2, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") },
            //    new Invoice { InvoiceId = 3, CreateDate = DateTime.Now, Status = InvoiceStatus.Success, UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749") }
            //);
            #endregion
            #region PaymentInfo
            //modelBuilder.Entity<PaymentInfo>().HasData(
            //    new PaymentInfo { PaymentInfoId = 1, InvoiceId = 1, PaymentMethod = PaymentMethod.VNPay, PaymentStatus = PaymentStatus.Success },
            //    new PaymentInfo { PaymentInfoId = 2, InvoiceId = 2, PaymentMethod = PaymentMethod.MomoQR, PaymentStatus = PaymentStatus.Success },
            //    new PaymentInfo { PaymentInfoId = 3, InvoiceId = 3, PaymentMethod = PaymentMethod.Cash, PaymentStatus = PaymentStatus.Success }
            //);
            #endregion
            #region Role
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                //new IdentityRole<Guid> { Id = Guid.Parse("30A990C6-33C7-4884-9DCB-718CE356EB0D"), Name = "SupperAdmin", NormalizedName = "SupperAdmin" },
                new IdentityRole<Guid> { Id = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Id = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E"), Name = "User", NormalizedName = "USER" }
            );
            #endregion
            #region UserRole
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                //Seed admin
                new IdentityUserRole<Guid> { UserId = Guid.Parse("2753c921-2304-4f8d-b8d5-75229d3b20d6"), RoleId = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA") },
                //Seend user
                new IdentityUserRole<Guid> { UserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"), RoleId = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E") }
            );
            #endregion
            #region TimeRange
            modelBuilder.Entity<TimeRange>().HasData(
                new TimeRange { Id = 1, Name = "0 - 1 triệu", MinPrice = 0, MaxPrice = 1000000 },
                new TimeRange { Id = 2, Name = "1 triệu - 2 triệu", MinPrice = 1000000, MaxPrice = 2000000 },
                new TimeRange { Id = 3, Name = "2 triệu - 3 triệu", MinPrice = 2000000, MaxPrice = 3000000 },
                new TimeRange { Id = 4, Name = "3 triệu - 4 triệu", MinPrice = 3000000, MaxPrice = 4000000 },
                new TimeRange { Id = 5, Name = "4 triệu - 5 triệu", MinPrice = 4000000, MaxPrice = 5000000 },
                new TimeRange { Id = 6, Name = "5 triệu - 6 triệu", MinPrice = 5000000, MaxPrice = 6000000 },
                new TimeRange { Id = 7, Name = "6 triệu - 7 triệu", MinPrice = 6000000, MaxPrice = 7000000 },
                new TimeRange { Id = 8, Name = "7 triệu - 8 triệu", MinPrice = 7000000, MaxPrice = 8000000 },
                new TimeRange { Id = 9, Name = "8 triệu - 9 triệu", MinPrice = 8000000, MaxPrice = 9000000 },
                new TimeRange { Id = 10, Name = "9 triệu - 10 triệu", MinPrice = 9000000, MaxPrice = 10000000 },
                new TimeRange { Id = 11, Name = "Hơn 10 triệu", MinPrice = 10000000, MaxPrice = 999999999 }
            );
            #endregion
            #region CategoryTimeRange
            modelBuilder.Entity<CategoryTimeRange>().HasData(
                new CategoryTimeRange { Id = 1, CategoryId = 15, TimeRangeId = 1 },
                new CategoryTimeRange { Id = 2, CategoryId = 15, TimeRangeId = 2 },
                new CategoryTimeRange { Id = 4, CategoryId = 15, TimeRangeId = 4 },
                new CategoryTimeRange { Id = 5, CategoryId = 15, TimeRangeId = 5 },
                new CategoryTimeRange { Id = 6, CategoryId = 15, TimeRangeId = 6 },
                new CategoryTimeRange { Id = 7, CategoryId = 15, TimeRangeId = 7 },
                new CategoryTimeRange { Id = 8, CategoryId = 15, TimeRangeId = 8 },
                new CategoryTimeRange { Id = 9, CategoryId = 15, TimeRangeId = 9 },
                new CategoryTimeRange { Id = 11, CategoryId = 15, TimeRangeId = 11 }
            );
            #endregion
            #region Voucher
            modelBuilder.Entity<Voucher>().HasData(
        new Voucher
        {
            Id = 1,
            Code = "VOUCHER10",
            Description = "10% off",
            Quantity = 100,
            QuantityUsed = 0,
            UsageLimit = 10,
            MinOrderAmount = 100,
            MaxDiscountAmount = 50,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            IsActive = true,
            DiscountType = DiscountType.Percent,
            DiscountAmount = 10
        },
        new Voucher
        {
            Id = 2,
            Code = "VOUCHER20",
            Description = "20% off",
            Quantity = 200,
            QuantityUsed = 0,
            UsageLimit = 20,
            MinOrderAmount = 200,
            MaxDiscountAmount = 100,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(2),
            IsActive = true,
            DiscountType = DiscountType.Percent,
            DiscountAmount = 20
        }
    );
            #endregion
            #region VoucherUser
            modelBuilder.Entity<VoucherUser>().HasData(
            new VoucherUser
            {
                Id = 1,
                VoucherId = 1,
                AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                UsageCount = 0,
                IsDeleted = false
            },
            new VoucherUser
            {
                Id = 2,
                VoucherId = 2,
                AppUserId = Guid.Parse("00bb44d1-f674-49f6-bdae-afb143ab9749"),
                UsageCount = 0,
                IsDeleted = false
            }
        );
            #endregion ShppingOrder
            #region VoucherCate
            modelBuilder.Entity<VoucherCate>().HasData(
                new VoucherCate { Id = 1,VoucherId = 1, CategoryId = 1 },
                new VoucherCate { Id = 2,VoucherId = 1, CategoryId = 2 }
    );
            #endregion
            #region VoucherProduct
            modelBuilder.Entity<VoucherProduct>().HasData(
                new VoucherProduct { Id = 1, VoucherId = 1, ProductId = 1 },
                new VoucherProduct { Id = 2, VoucherId = 2, ProductId = 2 }
            );
            #endregion
            #region ShippingOrder
            //modelBuilder.Entity<ShippingOrder>().HasData(
            //    new ShippingOrder { Id = 1, OrderCode = "L6AHHM", UserId = Guid.Parse("AA7C5218-4F1E-4AC6-A3B4-08DCB162E29E"), Price = 200000, CreateAt = DateTime.Now, InvoiceId = 3 }
            //);
            #endregion
            #region ProductPromotion
            modelBuilder.Entity<ProductPromotion>().HasData(
                new ProductPromotion { ProductPromotionId = 1, PromotionId = 11, ProductId = 1 },
                new ProductPromotion { ProductPromotionId = 2, PromotionId = 11, ProductId = 2 }
            // Add more seed data as needed
            );
            #endregion


            #region ProductEAV
            // Seed Product
            modelBuilder.Entity<Product_EAV>().HasData(
                new Product_EAV { ProductId = 1, ProductName = "Samsung Smart TV QLED QA55Q70C",OriginId =1,Status = ProductStatus.Sale,BrandId =2 }
            );
            modelBuilder.Entity<Product_EAV>().HasData(
                new Product_EAV { ProductId = 2, ProductName = "Tủ lạnh LG Inverter Multi Door GR-B50BL",OriginId=1,Status = ProductStatus.Sale,BrandId =1 }
            );

            // Seed Attributes
            modelBuilder.Entity<Attribute_EAV>().HasData(
                new Attribute_EAV { AttributeId = 1, AttributeName = "Màu sắc" },
                new Attribute_EAV { AttributeId = 2, AttributeName = "Kích thước" },
                new Attribute_EAV { AttributeId = 3, AttributeName = "Dung tích" }
            );

            // Seed AttributeValues
            modelBuilder.Entity<AttributeValue_EAV>().HasData(
                new AttributeValue_EAV { AttributeValueId = 1, AttributeId = 1, ValueText = "Đen" },
                new AttributeValue_EAV { AttributeValueId = 2, AttributeId = 1, ValueText = "Trắng" },
                new AttributeValue_EAV { AttributeValueId = 3, AttributeId = 2, ValueText = "50 inch" },
                new AttributeValue_EAV { AttributeValueId = 4, AttributeId = 2, ValueText = "60 inch" },
                new AttributeValue_EAV { AttributeValueId = 5, AttributeId = 3, ValueText = "40 lít" },
                new AttributeValue_EAV { AttributeValueId = 6, AttributeId = 3, ValueText = "50 lít" }
            );

            // Seed Variants
            modelBuilder.Entity<Variant>().HasData(
                new Variant
                {
                    VariantId = 1,
                    ProductId = 1,
                    VariantName = "Đen/50 inch",
                    Quantity = 100,
                    PuscharPrice = 50000, // Giá nhập
                    SalePrice = 75000,   // Giá bán
                    AfterDiscountPrice = 70000,
                    IsDefault = true,
                },
                new Variant
                {
                    VariantId = 2,
                    ProductId = 1,
                    VariantName = "Đen/60 inch",
                    Quantity = 50,
                    PuscharPrice = 52000,
                    SalePrice = 78000,
                    AfterDiscountPrice = 71000,
                    IsDefault = false
                },
                new Variant
                {
                    VariantId = 3,
                    ProductId = 1,
                    VariantName = "Trắng/50 inch",
                    Quantity = 75,
                    PuscharPrice = 48000,
                    SalePrice = 73000,
                    AfterDiscountPrice = 70500,
                    IsDefault = false
                },
                new Variant
                {
                    VariantId = 4,
                    ProductId = 1,
                    VariantName = "Trắng/60 inch",
                    Quantity = 80,
                    PuscharPrice = 49000,
                    SalePrice = 74000,
                    AfterDiscountPrice = 71000,
                    IsDefault = false
                },
                new Variant
                {
                    VariantId = 5,
                    ProductId = 2,
                    VariantName = "Đen/40 lít",
                    Quantity = 0,
                    PuscharPrice = 49000,
                    SalePrice = 74000,
                    AfterDiscountPrice = 71000,
                    IsDefault = true
                },
                new Variant
                {
                    VariantId = 6,
                    ProductId = 2,
                    VariantName = "Đen/50 lít",
                    Quantity = 80,
                    PuscharPrice = 49000,
                    SalePrice = 74000,
                    AfterDiscountPrice = 71000,
                    IsDefault= false
                }
            );

            // Seed VariantAttributes
            modelBuilder.Entity<VariantAttribute>().HasData(
                // Variant 1
                new VariantAttribute { VariantAttributeId = 1, VariantId = 1, AttributeValueId = 1 }, // Đen
                new VariantAttribute { VariantAttributeId = 2, VariantId = 1, AttributeValueId = 3 }, // 50inch

                // Variant 2
                new VariantAttribute { VariantAttributeId = 3, VariantId = 2, AttributeValueId = 1 }, // Đen
                new VariantAttribute { VariantAttributeId = 4, VariantId = 2, AttributeValueId = 4 }, // 60inch

                // Variant 3
                new VariantAttribute { VariantAttributeId = 5, VariantId = 3, AttributeValueId = 2 }, // Trắng
                new VariantAttribute { VariantAttributeId = 6, VariantId = 3, AttributeValueId = 3 }, // 50inch

                // Variant 4
                new VariantAttribute { VariantAttributeId = 7, VariantId = 4, AttributeValueId = 2 }, // Trắng
                new VariantAttribute { VariantAttributeId = 8, VariantId = 4, AttributeValueId = 4 }, // 60inch

                // Variant 5
                new VariantAttribute { VariantAttributeId = 9, VariantId = 5, AttributeValueId = 1 }, // Trắng
                new VariantAttribute { VariantAttributeId = 10, VariantId = 5, AttributeValueId = 5 },  // 60inch

                // Variant 6
                new VariantAttribute { VariantAttributeId = 11, VariantId = 6, AttributeValueId = 1 }, // Trắng
                new VariantAttribute { VariantAttributeId = 12, VariantId = 6, AttributeValueId = 6 }  // 60inch
            );
            #endregion
        }

    }


}

