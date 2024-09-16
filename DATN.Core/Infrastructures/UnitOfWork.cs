using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using DATN.Core.Repositories.Repositories;
using DATN.Core.Repositories.Repositories.ProductEAV;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace DATN.Core.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DATNDbContext _context;

        //Them repo

        private IUserRepository _userRepository;


        private ICategoryRepository _categoryRepository;
        private ICategoryProductRepository _categoryProductRepository;
        private IPromotionRepository _promotionRepository;
        private IPendingCartRepository _pendingCartRepository;
        private IPendingCartVariantRepository _pendingCartVariantRepository;

        private IAuthenRepository _authenRepository;
        private IRoleRepository _roleRepository;
        //Product
        private IimageRepository _imageReponsiroty;
        private IProductPromotionRepository _productPromotionRepository;

        private IInvoiceDetailRepository _invoiceDetailRepository;

        private IOriginRepositoty _originRepositoty;
        private IMagazineRepository _magazineRepository;

        private IBrandRepository _brandRepository;
        private IInvoiceRepository _invoiceRepository;
        private IPaymentInfoRepository _paymentInfoRepository;

        private ICommentRepository _commentRepository;
        private ITimeRangeRepository _timeRangeRepository;
        private IShippingOrderRepository _shippingOrderRepository;
        private ICategoryTimeRangeRepository _CateRangeRepository;
        private IVoucherRepository _voucherRepository;
        private IBatchRepository _batchRepository;


        private IAttributeEAVRepository _attributeEAVRepository;
        private IProductEAVRepository _productEAVRepository;
        private IAttributeValueEAVRepository _attributeValueEAVRepository;
        private IVariantAttributeRepository _variantAttributeRepository;
        private IVariantRepository _variantRepository;
        private ISpecificationRepository _specificationRepository;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DATNDbContext context, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        //Thêm tương tự
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context, _mapper));
        public ICategoryProductRepository CategoryProductRepository => _categoryProductRepository ?? (_categoryProductRepository = new CategoryProductRepository(_context, _mapper));
        public IPromotionRepository PromotionRepository => _promotionRepository ?? (_promotionRepository = new PromotionRepository(_context, _mapper));
        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(_context));
        //Product
        public IInvoiceDetailRepository InvoiceDetailRepository => _invoiceDetailRepository ?? (_invoiceDetailRepository = new InvoiceDetailRepository(_context, _mapper));
        public IPendingCartRepository PendingCartRepository => _pendingCartRepository ?? (_pendingCartRepository = new PendingCartRepository(_context, _mapper));
        public IPendingCartVariantRepository PendingCartVariantRepository => _pendingCartVariantRepository ?? (_pendingCartVariantRepository = new PendingCartVariantRepository(_context, _mapper));


        public IimageRepository imageReponsiroty => _imageReponsiroty ?? (_imageReponsiroty = new ImageReponsiroty(_context, _mapper));
        public IProductPromotionRepository productPromotionRepository => _productPromotionRepository ?? (_productPromotionRepository = new ProductPromotionRepository(_context, _mapper));
        public IOriginRepositoty originRepositoty => _originRepositoty ?? (_originRepositoty = new OriginRepository(_context, _mapper));


        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context, _mapper));
        public DATNDbContext AppDbContext => _context;

        public IAuthenRepository AuthenRepository => _authenRepository ?? (_authenRepository = new AuthenRepository(_userManager, _roleManager, _context, _configuration));

        public IBrandRepository brandRepository => _brandRepository ?? (_brandRepository = new BrandRepository(_context, _mapper));
        public ICommentRepository commentRepository => _commentRepository ?? (_commentRepository = new CommentRepository(_context, _mapper));
        public IInvoiceRepository InvoiceRepository => _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(_context, _mapper));
        public IMagazineRepository MagazineRepository => _magazineRepository ?? (_magazineRepository = new MagazineRepository(_context, _mapper));

        public IPaymentInfoRepository PaymentInfoRepository => _paymentInfoRepository ?? (_paymentInfoRepository = new PaymentInfoRepository(_context, _mapper));

        public ITimeRangeRepository TimeRangeRepository => _timeRangeRepository ?? (_timeRangeRepository = new TimeRangeRepository(_context, _mapper));


        public ICategoryTimeRangeRepository CategoryTimeRange => _CateRangeRepository ?? (_CateRangeRepository = new CategoryTimerangeRepository(_context, _mapper));

        public IShippingOrderRepository ShippingOrderRepository => _shippingOrderRepository ?? (_shippingOrderRepository = new ShippingOrderRepository(_context, _mapper));
        public IVoucherRepository VoucherRepository => _voucherRepository ?? (_voucherRepository = new VoucherRepository(_context, _mapper));



        public IAttributeEAVRepository AttributeEAVRepository => _attributeEAVRepository ?? (_attributeEAVRepository = new AttributeEAVRepository(_context, _mapper));
        public IProductEAVRepository ProductEAVRepository => _productEAVRepository ?? (_productEAVRepository = new ProductEAVRepository(_context, _mapper));
        public IAttributeValueEAVRepository AttributeValueEAVRepository => _attributeValueEAVRepository ?? (_attributeValueEAVRepository = new AttributeValueEAVRepository(_context, _mapper));
        public IVariantAttributeRepository VariantAttributeRepository => _variantAttributeRepository ?? (_variantAttributeRepository = new VariantAttributeRepository(_context, _mapper));
        public IVariantRepository VariantRepository => _variantRepository ?? (_variantRepository = new VariantRepository(_context, _mapper));

        public IBatchRepository BatchRepository => (_batchRepository = new BatchRepository(_context,_mapper));

        public ISpecificationRepository SpecificationRepository => _specificationRepository ?? (_specificationRepository = new SpecificationRepository(_context, _mapper));


        // Begin a database transaction
        public IDbContextTransaction BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return _transaction;
        }

        public void Commit()
        {
            _transaction?.Commit();
        }


        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


    }
}
