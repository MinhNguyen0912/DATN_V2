using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.InvoiceDetailVM;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{

    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public InvoiceDetailRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<InvoiceDetail> GetAll()
        {
            return _context.InvoiceDetails.Include(p => p.Variant).Include(p => p.Comment);
        }
        public List<InvoiceDetail> FincByInvoiceIdCustom(int invoiceId)
        {
            return _context.InvoiceDetails.Where(p=>p.InvoiceId == invoiceId).Include(p=>p.Variant).ThenInclude(p=>p.Product).ToList();
        }
        public List<InvoiceDetailForCommentVM> GetInvoiceDetailByInvoiceId(int invoiceId, Guid userId)
        {
            //try
            //{
            //    var InvoinceDetail = _context.InvoiceDetails.Where(x => x.InvoiceId == invoiceId).Include(p => p.Variant).ThenInclude(p => p.Product).ToList();
            //    var invoiceDetailForCommentVms = _mapper.Map<List<InvoiceDetailForCommentVM>>(InvoinceDetail);
            //    var ProductAttributes = InvoinceDetail.Select(c => c.VariantId);
            //    var listAtt = _context.ProductAttributes.AsQueryable().Where(c => ProductAttributes.Contains(c.Id)).ToList();
            //    var listProductId = listAtt.Select(c => c.ProductId);
            //    var listProduct = _context.Products.AsQueryable().Where(c => listProductId.Contains(c.Id));
            //    var listImageByArrProuductId = _context.Images.AsQueryable().Where(c => listProductId.Contains((int)c.ProductId) && c.TypeId == 1).ToList();
            //    var lstComment = _context.Comments.AsQueryable()
            //        .Where(c => listProductId.Contains(c.ProductId)).ToList();
            //    foreach (var x in invoiceDetailForCommentVms)
            //    {
            //        x.ProductId = listAtt.FirstOrDefault(c => c.Id == x.ProductAttributeId).ProductId;
            //        x.ImagePath = listImageByArrProuductId.FirstOrDefault(c => c.ProductId == x.ProductId).ImagePath;
            //        x.ProductName = listProduct.FirstOrDefault(c => c.Id == Convert.ToInt32(x.ProductId)).Name;

            //        x.IsShowComment = lstComment
            //            .Any(c => c.ProductId == x.ProductId && c.InvoiceDetailId == x.InvoiceDetailId) ? false : true;
            //    }

            //    return invoiceDetailForCommentVms;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
            return null;
        }
    }
}
