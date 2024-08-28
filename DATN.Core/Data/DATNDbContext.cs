

using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ModelConfigurations;
using DATN.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DATN.Core.Data
{
    public class DATNDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DATNDbContext()
        {

        }
        public DATNDbContext(DbContextOptions<DATNDbContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryProduct> categoryProducts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherUser> VoucherUsers { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }
        public DbSet<CategoryTimeRange> CategoryTimeRange { get; set; }
        public DbSet<ShippingOrder> ShippingOrders { get; set; }

        public DbSet<Attribute_EAV> Attribute_EAVs { get; set; }
        public DbSet<AttributeValue_EAV> AttributeValue_EAVs { get; set; }
        public DbSet<Product_EAV> Product_EAVs { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<VariantAttribute> VariantAttributes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DATNDbContextConnection"));
                //optionsBuilder.UseSqlServer("server =ADMIN\\DUC; database = MomoProject;uid=sa;pwd=12345;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());



            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());


            //using (var context = new DATNDbContext())
            //{
            //    if (!InitializationData.IsSeeded(context))
            //    {
            //        modelBuilder.Seed();
            //    }
            //}
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
