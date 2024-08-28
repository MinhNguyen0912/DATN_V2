using DATN.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATN.Core.ModelConfigurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder
             .HasOne(id => id.Invoice)
             .WithMany(i => i.InvoiceDetails)
             .HasForeignKey(id => id.InvoiceId);
            builder
            .HasOne(id => id.Invoice)
            .WithMany(i => i.InvoiceDetails)
            .HasForeignKey(id => id.InvoiceId);

            builder.HasOne(c => c.Comment).WithOne(p => p.InvoiceDetail).HasForeignKey<Comment>(p => p.InvoiceDetailId);
        }
    }
}
