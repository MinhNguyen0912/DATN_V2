﻿using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Utitlities.FormatCurrency;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using DATN.Core.ViewModels.UserViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public UserRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public AppUser GetByIdCustom(Guid userId)
        {
            return _context.Users.Where(p => p.Id == userId).Include(p => p.PendingCart).ThenInclude(p => p.PendingCartVariants).ThenInclude(p=>p.Variant).ThenInclude(p=>p.Product).ThenInclude(p=>p.Images).FirstOrDefault();
        }
        public UserPaging GetUserPaging(UserPaging request)
        {
            try
            {
 var query = Context.Users.AsQueryable();
            if (request.LastLoginTimeFilter != LastLoginTimeFilter.All && request.LastLoginTimeFilter != null)
            {
                DateTime filterTime = DateTime.Now;

                switch (request.LastLoginTimeFilter)
                {
                    case LastLoginTimeFilter.LastWeek:
                        filterTime = DateTime.Now.AddDays(-7);
                        break;
                    case LastLoginTimeFilter.LastMonth:
                        filterTime = DateTime.Now.AddMonths(-1);
                        break;
                    case LastLoginTimeFilter.LastFourMonths:
                        filterTime = DateTime.Now.AddMonths(-4);
                        break;
                    case LastLoginTimeFilter.LastYear:
                        filterTime = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        break;
                }

                query = query.Where(x => x.LastLoginTime >= filterTime && x.LastLoginTime <= DateTime.Now);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.FullName.Contains(searchTerm) ||
                                         x.Email.Contains(searchTerm) || x.PhoneNumber.Contains(searchTerm));
            }


            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            var listUserId = list.Select(c => c.Id).ToList();
            var lstInvoiceByUserId = Context.Invoices.AsQueryable().Where(c => listUserId.Contains(c.UserId) && c.Status==InvoiceStatus.Success).ToList();

            var lstVoucher = Context.Vouchers.AsQueryable();
            var lstBatches = Context.Batches.AsQueryable();
            request.Items = _mapper.Map<List<UserVM>>(list);
            FormatCurrency formatCurrency = new FormatCurrency();
            foreach (var x in request.Items)
            {
                List<decimal> listFinalPrice = new List<decimal>();
                
                foreach (var invoice in lstInvoiceByUserId.Where(c => c.UserId == x.Id))
                {
                    listFinalPrice.Add((invoice.FinalAmount.HasValue?(decimal)invoice.FinalAmount:0));

                }
                x.GrandTotalAmountPurchased = formatCurrency.GetCurrency(
                    Convert.ToDecimal(listFinalPrice.Sum(c => c)));
                x.ListVoucherNameByUser = lstBatches.Where(c => lstVoucher.Where(c=>c.UserId==x.Id).Select(c=>c.BatchId).Contains(c.Id) && c.IsActive==true).Select(c => c.Name).ToList();
            }

            //request.ListVoucherDropDown = _mapper.Map<List<VoucherVM>>(lstVoucher);
            var xx= request;
            return request;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        //public async Task<int> AddVoucherToListUser(List<UserVoucherShowModal> input)
        //{
        //    List<VoucherUser> voucherUsers = new List<VoucherUser>();
        //    foreach (var x in input)
        //    {
        //        if (!await Context.VoucherUsers.AnyAsync(c =>
        //                c.AppUserId == x.UserId && c.VoucherId == Convert.ToInt32(x.VoucherId)))
        //        {
        //            VoucherUser voucherUser = new VoucherUser();
        //            voucherUser.VoucherId = Convert.ToInt32(x.VoucherId);
        //            voucherUser.AppUserId = x.UserId;
        //            voucherUsers.Add(voucherUser);
        //        }
        //    }

        //    await Context.VoucherUsers.AddRangeAsync(voucherUsers);
        //    await Context.SaveChangesAsync();
        //    return voucherUsers.Count();
        //}

        public bool DeleteUser(Guid userId)
        {
            var user = Context.Users.FirstOrDefault(d => d.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.isActive = !user.isActive;
            Context.SaveChanges();
            return user.isActive;
        }

        public int UpdateUser(AppUser user)
        {
            var existingUser = Context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return 0;
            }
            else
            {
                Context.Entry(existingUser).CurrentValues.SetValues(user);
            }

            return Context.SaveChanges();
        }

        public AppUser GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email);
        }

        public async Task<List<AppUser>> SearchUser(string search)
        {
            var res=await Context.Users.AsQueryable().Where(c=>c.PhoneNumber.ToLower().Contains(search.ToLower()) || c.FullName.ToLower().Contains(search.ToLower())).ToListAsync();
            return res;
        }

        //public async Task<IEnumerable<string>> GetListVoucherByUserId(Guid userId)
        //{
        //    var lstVoucherUsers = Context.VoucherUsers.AsQueryable().Where(c => c.AppUserId == userId)
        //        .ToList();
        //    var lstVoucherId = lstVoucherUsers.Select(c => c.VoucherId);
        //    var lstVoucher = Context.Vouchers.AsQueryable();
        //    var lstVoucherById = lstVoucher.Where(c => lstVoucherId.Contains(c.Id)).ToList();
        //    var result = lstVoucherById.Select(c => c.Name);
        //    return result;
        //}

        public IEnumerable<AppUser> GetUsersByIds(List<Guid> userIds)
        {
            return Context.Users.Where(u => userIds.Contains(u.Id)).ToList();
        }

        public List<AppUser> GetUsersExport(string search, string lastLoginTimeFilter)
        {
            var query = Context.Users.Where(x => x.isActive == true).AsQueryable();
            if (lastLoginTimeFilter != LastLoginTimeType.All && lastLoginTimeFilter != null)
            {
                DateTime filterTime = DateTime.Now;

                switch (lastLoginTimeFilter)
                {
                    case LastLoginTimeType.LastWeek:
                        filterTime = DateTime.Now.AddDays(-7);
                        break;
                    case LastLoginTimeType.LastMonth:
                        filterTime = DateTime.Now.AddMonths(-1);
                        break;
                    case LastLoginTimeType.LastFourMonths:
                        filterTime = DateTime.Now.AddMonths(-4);
                        break;
                    case LastLoginTimeType.LastYear:
                        filterTime = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        break;
                }

                query = query.Where(x => x.LastLoginTime >= filterTime && x.LastLoginTime <= DateTime.Now);
                if (!string.IsNullOrEmpty(search))
                {
                    string searchTerm = search.Trim().ToLower();
                    query = query.Where(x => x.FullName.Contains(searchTerm) ||
                                             x.Email.Contains(searchTerm) || x.PhoneNumber.Contains(searchTerm));
                }
            }

            return query.ToList();
        }

        public UserProfile GetUserProfile(UserProfile userProfile)
        {
            UserProfile response = new UserProfile();
            var user = Context.Users.Where(x => x.Id == userProfile.UserVM.Id).FirstOrDefault();
            response.UserVM = _mapper.Map<UserExportVM>(user);
            return response;
        }
    }
}