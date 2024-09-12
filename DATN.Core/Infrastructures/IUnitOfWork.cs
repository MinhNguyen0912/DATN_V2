

using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.IRepositories.ProductEAV;

namespace DATN.Core.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public ICategoryProductRepository CategoryProductRepository { get; }
        public IUserRepository UserRepository { get; }

        public IimageRepository imageReponsiroty { get; }
        public IProductPromotionRepository productPromotionRepository { get; }


        public IOriginRepositoty originRepositoty { get; }
        public IPromotionRepository PromotionRepository { get; }
        public IInvoiceDetailRepository InvoiceDetailRepository { get; }
        public IBrandRepository brandRepository { get; }
        public ICommentRepository commentRepository { get; }
        public IMagazineRepository MagazineRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }
        public IPaymentInfoRepository PaymentInfoRepository { get; }
        public IAuthenRepository AuthenRepository { get; }
        public ITimeRangeRepository TimeRangeRepository { get; }

        public ICategoryTimeRangeRepository CategoryTimeRange { get; }
        public IShippingOrderRepository ShippingOrderRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IVoucherRepository VoucherRepository { get; }
        public IBatchRepository BatchRepository { get; }
        public IPendingCartRepository PendingCartRepository { get; }
        public IPendingCartVariantRepository PendingCartVariantRepository { get; }
        //public ICategoryRepository CategoryRepository { get; } // read only
        //public IProjectRepository ProjectRepository { get; }
        //public IPartnerRepository PartnerRepository { get; }
        //public IProjectImageRepository ProjectImageRepository { get; }
        //public MomoDbContext AppDbContext { get; }
        //public IAuthenRepository AuthenRepository { get; }
        //public IRoleRepository RoleRepository { get; }
        //public IUserRepository UserRepository { get; }
        //public IUserFollowRepository UserFollowRepository { get; }
        public IAttributeEAVRepository AttributeEAVRepository { get; }
        public IProductEAVRepository ProductEAVRepository { get; }
        public IAttributeValueEAVRepository AttributeValueEAVRepository { get; }
        public IVariantAttributeRepository VariantAttributeRepository { get; }
        public IVariantRepository VariantRepository { get; }

        int SaveChanges();
    }
}
