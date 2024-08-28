using DATN.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATN.Core.ModelConfigurations
{
    public class PaymentInfoConfiguration : IEntityTypeConfiguration<PaymentInfo>
    {
        public void Configure(EntityTypeBuilder<PaymentInfo> builder)
        {
        }
    }
}
