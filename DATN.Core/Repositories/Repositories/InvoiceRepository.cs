using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.Paging;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        private readonly IMapper _mapper;
        public InvoiceRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Invoice> GetInvoiceByUserId(Guid userId)
        {
            return Context.Invoices.Where(x => x.UserId == userId).Include(d => d.InvoiceDetails).ToList();
        }
        //public Invoice GetByIdCustom(int id)
        //{
        //    return Context.Invoices.Where(x => x.InvoiceId == id).Include(u => u.User).Include(d => d.InvoiceDetails).ThenInclude(p => p.Variant).ThenInclude(p => p.Product).Include(d => d.InvoiceDetails).ThenInclude(p => p.ProductAttribute).ThenInclude(p => p.AttributeValue).Include(p => p.VoucherUser).ThenInclude(p => p.Voucher).Include(p => p.ShippingOrder).FirstOrDefault();
        //}
        public InvoicePaging GetInvoicePaging(InvoicePaging request)
        {
            var query = Context.Invoices.Include(p => p.User).Include(p => p.InvoiceDetails).Include(p=>p.PaymentInfo).OrderByDescending(p => p.CreateDate).AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => (x.User.UserName.ToLower().Contains(searchTerm)) || (x.User.FullName.Contains(searchTerm)));
            }
            if (request.Status != Enum.InvoiceStatus.All)
            {
                query = query.Where(p => p.Status == request.Status);
            }
            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<InvoiceVM>>(list);

            return request;
        }

        public List<InvoiceShowForClientVM> GetInvoiceByStatusAndUserId(Guid userId, InvoiceStatus status)
        {
            // Lấy danh sách hóa đơn với thông tin liên quan
            var invoices = Context.Invoices
                .Where(i => i.UserId == userId && i.Status == status)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(id => id.Variant)
                        .ThenInclude(pa => pa.Product)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(id => id.Comment)
                .Include(i => i.ShippingOrders)
                //.Include(i => i.VoucherUser)
                    //.ThenInclude(vu => vu.Voucher)
                .ToList();

            // Chuyển đổi sang InvoiceShowForClientVM bằng AutoMapper
            var invoiceShowForClientVMs = _mapper.Map<List<InvoiceShowForClientVM>>(invoices);

            // Cập nhật thuộc tính IsShowComment
            foreach (var invoice in invoiceShowForClientVMs)
            {
                foreach (var detail in invoice.InvoiceDetails)
                {
                    detail.IsShowComment = detail.Comment != null ? false : true;

                }
            }

            return invoiceShowForClientVMs;
        }

        public Invoice GetByIdCustom(int id)
        {
          return  Context.Invoices.Where(p=>p.InvoiceId== id).Include(p=>p.InvoiceDetails).ThenInclude(p=>p.Variant).ThenInclude(p=>p.Product).Include(p=>p.PaymentInfo).FirstOrDefault();
        }
        public List<Invoice> GetCustom()
        {
            return Context.Invoices.Include(p => p.PaymentInfo).ToList();
        }
    }
}
