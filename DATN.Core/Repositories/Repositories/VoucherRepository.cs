using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModel.VoucherVM;
using DATN.Core.ViewModels.UserViewModel;
using Hangfire.States;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public List<Voucher> GetAllVouchers()
        {
            return Context.Vouchers.Include(b => b.Batch).Include(u => u.User).ToList();
        }

        public Voucher GetByIdCustom(int id)
        {
            return Context.Vouchers.Include(b => b.Batch).Include(u => u.User).FirstOrDefault(x => x.Id == id);
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

        public List<Voucher> SearchVoucher(SearchVoucherRequest request)
        {
            var query = Context.Vouchers.Include(v => v.User).Where(x => x.BatchId == request.BatchId).ToList();
            if (request.Status != null)
            {
                query = query.Where(x => x.Status == request.Status).ToList();
            }
            if (request.UserName != null)
            {
                query = query.Where(x => x.User.UserName == request.UserName).ToList();
            }
            return query;
        }

        public async Task<string> GenerateVoucherAutoRegisterAsync(Guid userId)
        {
            try
            {
                // Create a list of vouchers
                var vouchers = new List<Voucher>
                {
                    new Voucher
                    {
                        Code = "UNEWAU" + GenerateVoucherCode(),
                        Status = VoucherStatus.NotUsed,
                        ReleaseDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddMonths(2),
                        ActivationTime = DateTime.Now,
                        UserId = userId,
                        BatchId = 6
                    },
                    new Voucher
                    {
                        Code = "FRSAU" + GenerateVoucherCode(),
                        Status = VoucherStatus.NotUsed,
                        ReleaseDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddMonths(2),
                        ActivationTime = DateTime.Now,
                        UserId = userId,
                        BatchId = 4
                    }
                };

                // Add vouchers to the context
                _context.Vouchers.AddRange(vouchers);

                // Save changes to the database
                var result = await _context.SaveChangesAsync();

                // Check the result and return an appropriate response
                if (result > 0)
                {
                    return "Voucher created successfully.";
                }
                else
                {
                    return "Failed to create vouchers.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return error message
                return $"An error occurred: {ex.Message}";
            }
        }

        public async Task<string> GenerateVoucherConditionAsync(Guid userId)
        {
            bool voucherCreated = false;

            // Kiểm tra điều kiện tổng giá trị đơn hàng
            //if (await IsCheckedTotalPuschasePriceAsync(userId))
            //{
            //    var voucher = new Voucher()
            //    {
            //        Code = "Free" + GenerateVoucherCode(),
            //        Status = VoucherStatus.NotUsed,
            //        ReleaseDate = DateTime.Now,
            //        ExpiryDate = DateTime.Now.AddMonths(2),
            //        ActivationTime = DateTime.Now,
            //        UserId = userId,
            //        BatchId = 4
            //    };
            //    _context.Add(voucher);
            //    voucherCreated = true;
            //}

            // Kiểm tra điều kiện số lần mua hàng
            if (await IsCheckedNumberBoughttAsync(userId))
            {
                var voucher = new Voucher()
                {
                    Code = "TOD" + GenerateVoucherCode(),
                    Status = VoucherStatus.NotUsed,
                    ReleaseDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddMonths(2),
                    ActivationTime = DateTime.Now,
                    UserId = userId,
                    BatchId = 2
                };
                _context.Add(voucher);
                voucherCreated = true;
            }

            // Lưu voucher vào cơ sở dữ liệu nếu có voucher được tạo
            if (voucherCreated)
            {
                await _context.SaveChangesAsync();
                return "Voucher created successfully.";
            }

            return "No voucher created. Conditions not met.";
        }


        //public async Task GenerateVoucherActivationTimeAsync(DateTime activationTime)
        //{
        //    // Lấy danh sách voucher có thời gian kích hoạt
        //    var vouchers = await _context.Vouchers
        //        .Where(v => v.ActivationTime == activationTime)
        //        .ToListAsync();

        //    foreach (var voucher in vouchers)
        //    {
        //        // Kiểm tra điều kiện và kích hoạt voucher
        //        if (DateTime.Now >= activationTime)
        //        {
        //            voucher.ActivationTime = activationTime;
        //            voucher.Status = VoucherStatus.Unpushlished;
        //            _context.Vouchers.Update(voucher);
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}

        private string GenerateVoucherCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
        }

        public async Task<bool> IsCheckedNumberBoughttAsync(Guid userId)
        {
            var totalOrders = await _context.Invoices
                                .Where(o => o.UserId == userId)
                                .CountAsync();

            if (totalOrders > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }


}
