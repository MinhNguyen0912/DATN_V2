using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.UserViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public VoucherRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public VoucherPaging GetVoucherPaging(VoucherPaging request)
        {
            var query = Context.Vouchers.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<VoucherVM>>(list);

            return request;
        }

        public async Task CreateVoucherAutoRgisterAsync(Guid UserId )
        {
            //List voucher or some vouchers
            var voucher = new Voucher()
            {
                Code = Guid.NewGuid().ToString(),
                Description = "Free Ship",
                Quantity = 5,
                QuantityUsed = 5,
                ActivationTime = DateTime.Now,
                MaxDiscountAmount = 5,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2),
                IsActive = true,
                DiscountType = DiscountType.Percent,
                //Id User
                VoucherUsers = new List<VoucherUser>()
                {
                   
                }
            };
        }


        public async Task CreateVoucherConditionAsync(AppUser user)
        {

            //Get User Condition
            var totalSpent = await _context.Invoices.Where(c => c.UserId == user.Id).SumAsync(p => p.FinalAmount);

            if (totalSpent >= 1000000)
            {
                var voucher = new Voucher()
                {
                    Code = GenerateVoucherCode(),
                    Description = "Free Ship",
                    Quantity = 2,
                    ActivationTime = DateTime.Now,
                    MaxDiscountAmount = 5,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(2),
                    IsActive = true,
                    DiscountType = DiscountType.Percent,
                    //Id User
                    VoucherUsers = new List<VoucherUser>()
                    {
                
                    }
                };
                _context.Add(voucher);
                await _context.SaveChangesAsync();  
            }
           
        }

        public async Task CreateVoucherActivationTimeAsync(DateTime ActivationTime)
        {

            //Get list voucher ActivationTime 

            //For 
            //if (Condition >= )
            //{

            //}
            var voucher = new Voucher()
            {
                Code = Guid.NewGuid().ToString(),
                Description = "Free Ship",
                Quantity = 5,
                QuantityUsed = 5,
                ActivationTime = DateTime.Now,
                MaxDiscountAmount = 5,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2),
                IsActive = true,
                DiscountType = DiscountType.Percent,
                //Id User
                VoucherUsers = new List<VoucherUser>()
                {

                }
            };
        }
        private string GenerateVoucherCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }


}
