using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.StatisticAdminVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly DATNDbContext _context;
        public StatisticRepository(DATNDbContext context)
        {
            _context = context;
        }
        private List<DateTime> GetDaysInMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var daysInMonth = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                        .Select(day => startDate.AddDays(day))
                                        .ToList();

            return daysInMonth;
        }
        private List<InvoiceDetailDTO> GetInvoiceDetails(DateTime startDate, DateTime endDate)
        {
            var query = from invoice in _context.Invoices
                        join shippingOrder in _context.ShippingOrders
                        on invoice.InvoiceId equals shippingOrder.InvoiceId into shippingOrdersGroup
                        from shippingOrder in shippingOrdersGroup.DefaultIfEmpty()
                        join detail in _context.InvoiceDetails
                        on invoice.InvoiceId equals detail.InvoiceId into detailsGroup
                        from detail in detailsGroup.DefaultIfEmpty()
                        where invoice.Status == InvoiceStatus.Success ||
                              invoice.Status == InvoiceStatus.Delivery
                        group new { invoice, shippingOrder, detail } by new
                        {
                            invoice.InvoiceId,
                            invoice.CreateDate,
                            invoice.VoucherId,
                            shippingOrderPrice = shippingOrder != null ? shippingOrder.Price : 0
                        } into g
                        select new InvoiceDetailDTO
                        {
                            CreateDate = g.Key.CreateDate,
                            Revenue =(g.Sum(x => x.detail.Quantity * x.detail.NewPrice) + (decimal)g.Key.shippingOrderPrice),
                            ShippingFee = (decimal)g.Key.shippingOrderPrice,
                            VoucherUserId = g.Key.VoucherId
                        };

            var result = query
                .Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate)
                .ToList();

            return result;
        }

        private List<IncomeByInvoiceDTO> GetInvoiceIncome(DateTime startDate, DateTime endDate)
        {
            var invoiceIncome = _context.Invoices
                .Where(invoice => invoice.Status == InvoiceStatus.Success || invoice.Status == InvoiceStatus.Delivery)
                .Join(_context.InvoiceDetails,
                    invoice => invoice.InvoiceId,
                    detail => detail.InvoiceId,
                    (invoice, detail) => new
                    {
                        invoice.CreateDate,
                        detail.Quantity,
                        detail.NewPrice,
                        detail.PuscharPrice,
                        invoice.VoucherId
                    })
                .Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate)
                .GroupBy(x => new { x.CreateDate.Date, x.VoucherId })
                .Select(g => new IncomeByInvoiceDTO
                {
                    Date = g.Key.Date,
                    TotalRevenue = g.Sum(x =>(x.Quantity * x.NewPrice)),
                    TotalIncome = g.Sum(x => (x.Quantity * (x.NewPrice - x.PuscharPrice))),
                    VoucherUserId = g.Key.VoucherId
                })
                .OrderBy(g => g.Date)
                .ToList();

            return invoiceIncome;
        }

        private List<IncomeByDayVM> CalculateIncomeByDay(List<IncomeByInvoiceDTO> data)
        {
            // Lấy tất cả các vouchers cùng với Batch của chúng
            var vouchers = _context.Vouchers
                .Include(v => v.Batch)
                .Where(v => v.Batch != null && v.Batch.Type == VoucherType.discount) // Lọc chỉ lấy các Batch có Type là Discount
                .ToList();

            // Tính toán thu nhập theo ngày
            var incomeByDay = data
                .GroupJoin(
                    vouchers, // Outer sequence
                    invoice => invoice.VoucherUserId, // Outer key selector (VoucherId trong invoice)
                    voucher => voucher.Id, // Inner key selector (Id của voucher)
                    (invoice, voucherGroup) => new
                    {
                        invoice.Date,
                        invoice.TotalRevenue,
                        invoice.TotalIncome,
                        Voucher = voucherGroup.FirstOrDefault() // Lấy voucher đầu tiên trong nhóm
                    })
                .Select(x => new
                {
                    x.Date,
                    x.TotalIncome,
                    x.TotalRevenue,
                    Batch = x.Voucher?.Batch, // Lấy Batch từ Voucher
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.Batch.DiscountType == DiscountType.Percent ?
                            (x.TotalRevenue * (x.Voucher.Batch.DiscountAmount ?? 0) / 100) :
                            (x.Voucher.Batch.DiscountAmount ?? 0)) : 0
                })
                .GroupBy(x => x.Date.Date)
                .Select(g => new IncomeByDayVM
                {
                    Date = g.Key,
                    TotalIncomeAfterDiscount = g.Sum(x => x.TotalIncome) - g.Sum(x => x.DiscountAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

            return incomeByDay;
        }
     private List<IncomeByMonthVM> CaculateIncomeByMonth(List<IncomeByInvoiceDTO> data, int year)
        {
            // Lấy tất cả các vouchers cùng với Batch của chúng
            var vouchers = _context.Vouchers
                .Include(v => v.Batch)
                .Where(v => v.Batch != null && v.Batch.Type == VoucherType.discount) // Lọc chỉ lấy các Batch có Type là Discount
                .ToList();

            // Tính toán thu nhập theo tháng
            var incomeByMonth = data
                .Where(invoice => invoice.Date.Year == year) // Lọc hóa đơn theo năm
                .GroupJoin(
                    vouchers, // Outer sequence
                    invoice => invoice.VoucherUserId, // Outer key selector (VoucherId trong hóa đơn)
                    voucher => voucher.Id, // Inner key selector (Id của voucher)
                    (invoice, voucherGroup) => new
                    {
                        invoice.Date,
                        invoice.TotalRevenue,
                        invoice.TotalIncome,
                        Voucher = voucherGroup.FirstOrDefault() // Lấy voucher đầu tiên trong nhóm
                    })
                .Select(x => new
                {
                    x.Date,
                    x.TotalIncome,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.Batch.DiscountType == DiscountType.Percent ?
                            (x.TotalRevenue * (x.Voucher.Batch.DiscountAmount ?? 0) / 100) :
                            (x.Voucher.Batch.DiscountAmount ?? 0)) : 0
                })
                .GroupBy(x => new { x.Date.Year, x.Date.Month })
                .Select(g => new IncomeByMonthVM
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncomeAfterDiscount = g.Sum(x => x.TotalIncome) - g.Sum(x => x.DiscountAmount)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            return incomeByMonth;
        }


        private List<RevenueByDayVM> CalculateRevenueByDay(List<InvoiceDetailDTO> invoiceDetails)
        {
            // Lấy tất cả các vouchers cùng với Batch của chúng
            var vouchers = _context.Vouchers
                .Include(v => v.Batch)
                .Where(v => v.Batch != null && v.Batch.Type == VoucherType.discount) // Lọc chỉ lấy các Batch có Type là Discount
                .ToList();

            // Tính toán doanh thu theo ngày
            var revenueByDay = invoiceDetails
                .GroupJoin(
                    vouchers, // Outer sequence
                    invoice => invoice.VoucherUserId, // Outer key selector (VoucherId trong hóa đơn chi tiết)
                    voucher => voucher.Id, // Inner key selector (Id của voucher)
                    (invoice, voucherGroup) => new
                    {
                        invoice.CreateDate,
                        invoice.Revenue,
                        Voucher = voucherGroup.FirstOrDefault() // Lấy voucher đầu tiên trong nhóm
                    })
                .Select(x => new
                {
                    x.CreateDate,
                    x.Revenue,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.Batch.DiscountType == DiscountType.Percent ?
                            (x.Revenue * (x.Voucher.Batch.DiscountAmount ?? 0) / 100) :
                            (x.Voucher.Batch.DiscountAmount ?? 0)) : 0
                })
                .GroupBy(x => x.CreateDate.Date)
                .Select(g => new RevenueByDayVM
                {
                    Date = g.Key,
                    Revenue = g.Sum(x => x.Revenue - x.DiscountAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

            return revenueByDay;
        }

        private List<RevenueByMonthVM> CalculateRevenueByMonth(List<InvoiceDetailDTO> invoiceDetails, int year)
        {
            // Lấy tất cả các vouchers cùng với Batch của chúng
            var vouchers = _context.Vouchers
                .Include(v => v.Batch)
                .Where(v => v.Batch != null && v.Batch.Type == VoucherType.discount) // Lọc chỉ lấy các Batch có Type là Discount
                .ToList();

            // Tính toán doanh thu theo tháng
            var revenueByMonth = invoiceDetails
                .Where(invoice => invoice.CreateDate.Year == year) // Lọc hóa đơn theo năm
                .GroupJoin(
                    vouchers, // Outer sequence
                    invoice => invoice.VoucherUserId, // Outer key selector (VoucherId trong hóa đơn chi tiết)
                    voucher => voucher.Id, // Inner key selector (Id của voucher)
                    (invoice, voucherGroup) => new
                    {
                        invoice.CreateDate,
                        invoice.Revenue,
                        Voucher = voucherGroup.FirstOrDefault() // Lấy voucher đầu tiên trong nhóm
                    })
                .Select(x => new
                {
                    x.CreateDate,
                    x.Revenue,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.Batch.DiscountType == DiscountType.Percent ?
                            (x.Revenue * (x.Voucher.Batch.DiscountAmount ?? 0) / 100) :
                            (x.Voucher.Batch.DiscountAmount ?? 0)) : 0
                })
                .GroupBy(x => new { x.CreateDate.Year, x.CreateDate.Month })
                .Select(g => new RevenueByMonthVM
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(x => x.Revenue - x.DiscountAmount)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            return revenueByMonth;
        }


        public List<RevenueByDayVM> GetDailyRevenue(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn hóa đơn và chi tiết hóa đơn
                var invoiceDetails = GetInvoiceDetails(startDate, endDate);

            // Tính toán doanh thu theo ngày
            var revenueByDay = CalculateRevenueByDay(invoiceDetails);
            var daysInMonth = GetDaysInMonth(date);
            var revenueByDayWithAllDates = daysInMonth.Select(day => new RevenueByDayVM
            {
                Date = day,
                Revenue = revenueByDay.FirstOrDefault(r => r.Date == day)?.Revenue ?? 0
            }).ToList();

            return revenueByDayWithAllDates;
        }



        public List<RevenueByMonthVM> GetMonthlyRevenue(DateTime date)
        {
            // Xác định tháng và năm từ biến DateTime
            var year = date.Year;
            var month = date.Month;

            // Tạo ngày bắt đầu và ngày kết thúc của tháng
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn hóa đơn và chi tiết hóa đơn trong tháng
            var invoiceDetails = GetInvoiceDetails(startDate, endDate);

            // Tính toán doanh thu theo tháng
            var revenueByMonth = CalculateRevenueByMonth(invoiceDetails, year);

            // Tạo danh sách doanh thu cho từng tháng trong năm
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new RevenueByMonthVM
            {
                Year = year,
                Month = m,
                Revenue = revenueByMonth.FirstOrDefault(r => r.Month == m)?.Revenue ?? 0
            }).ToList();

            return monthsInYear;
        }

        public List<TopsellingProductVM> GetTopsellingProductInMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var topProducts = _context.InvoiceDetails
                .Where(id => id.Invoice.CreateDate >= startDate && id.Invoice.CreateDate <= endDate && (id.Invoice.Status == InvoiceStatus.Success || id.Invoice.Status == InvoiceStatus.Delivery))
                .GroupBy(id => id.Variant.ProductId) // Group theo ProductId
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(id => id.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .Take(5)
                .Join(_context.Product_EAVs, // Thay đổi bảng join
                      g => g.ProductId,
                      p => p.ProductId,
                      (g, p) => new TopsellingProductVM
                      {
                          ProductName = p.ProductName, // Lấy tên sản phẩm từ bảng Products
                          Quantity = g.TotalQuantity
                      })
                .ToList();

            return topProducts;
        }
        public List<TopsellingProductVM> GetTopsellingProductInWeek(DateTime date)
        {
            var startDate = date.Date.AddDays(-(int)(date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek) + (int)DayOfWeek.Monday);
            var endDate = startDate.AddDays(6).AddDays(1).AddSeconds(-1);

            var topProducts = _context.InvoiceDetails
                .Where(id => id.Invoice.CreateDate >= startDate
                              && id.Invoice.CreateDate <= endDate
                              && (id.Invoice.Status == InvoiceStatus.Success
                              || id.Invoice.Status == InvoiceStatus.Delivery))
                .GroupBy(id => id.Variant.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(id => id.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .Take(5)
                .Join(_context.Product_EAVs,
                      g => g.ProductId,
                      p => p.ProductId,
                      (g, p) => new TopsellingProductVM
                      {
                          ProductName = p.ProductName,
                          Quantity = g.TotalQuantity
                      })
                .ToList();

            return topProducts;
        }


        public List<IncomeByDayVM> GetDailyIncome(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var invoiceIncome = GetInvoiceIncome(startDate, endDate);
            var incomeByDay = CalculateIncomeByDay(invoiceIncome);
            var daysInMonth = GetDaysInMonth(date);
            var incomeByDayWithAllDates = daysInMonth.Select(day => new IncomeByDayVM
            {
                Date = day,
                TotalIncomeAfterDiscount = incomeByDay.FirstOrDefault(r => r.Date == day)?.TotalIncomeAfterDiscount ?? 0
            }).ToList();
            return incomeByDayWithAllDates;
        }
        public List<IncomeByMonthVM> GetMonthlyIncome(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var invoiceIncome = GetInvoiceIncome(startDate, endDate);
            var incomeByMonth = CaculateIncomeByMonth(invoiceIncome, year);
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new IncomeByMonthVM
            {
                Year = year,
                Month = m,
                TotalIncomeAfterDiscount = incomeByMonth.FirstOrDefault(r => r.Month == m)?.TotalIncomeAfterDiscount ?? 0
            }).ToList();
            return monthsInYear;
        }

        public List<AppGrowthByMonthVM> GetMonthlyAppGrowth(DateTime date)
        {
            var userGrowth = _context.Users
                .Where(u => u.LastLoginTime.Value.Year == date.Year && u.Description == "Customer")
                .GroupBy(u => u.LastLoginTime.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    UserCount = g.Count()
                }).ToList();
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new AppGrowthByMonthVM
            {
                Month = m,
                Year = date.Year,
                UserCount = userGrowth.FirstOrDefault(r => r.Month == m)?.UserCount ?? 0
            }).ToList();

            return monthsInYear;

        }

        public List<AppGrowthByDayVM> GetDailyAppGrowth(DateTime date)
        {
            var userGrowth = _context.Users
                .Where(u => u.LastLoginTime.Value.Year == date.Year && u.LastLoginTime.Value.Month == date.Month && u.Description == "Customer")
                .GroupBy(u => u.LastLoginTime.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    UserCount = g.Count()
                }).ToList();
            var daysInMonth = GetDaysInMonth(date);
            var userGrowthWithAllDates = daysInMonth.Select(day => new AppGrowthByDayVM
            {
                Date = day,
                UserCount = userGrowth.FirstOrDefault(r => r.Date == day)?.UserCount ?? 0
            }).ToList();
            return userGrowthWithAllDates;
        }
    }

}
