using DATN.Core.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Service
{
    public class VoucherService
    {
        private readonly DATNDbContext _context;

        public VoucherService(DATNDbContext dATNDbContext)
        {
            _context = dATNDbContext;
        }
        //public void ScheduleVoucherCreation(Guid userId, DateTime activationTime)
        //{
        //    BackgroundJob.Schedule(() => GenerateVoucherActivationTimeAsync(activationTime), activationTime);
        //}

        public async Task GenerateVoucherActivationTimeAsync(DateTime? activationTime)
        {
            // Lấy danh sách voucher có thời gian kích hoạt
            var vouchers = await _context.Vouchers
                .Where(v => v.ActivationTime == activationTime)
                .ToListAsync();

            foreach (var voucher in vouchers)
            {
                // Kiểm tra điều kiện và kích hoạt voucher
                if (DateTime.Now >= activationTime)
                {
                    voucher.Status = Enum.VoucherStatus.NotUsed;
                    voucher.ReleaseDate = DateTime.Now;
                    _context.Vouchers.Update(voucher);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task ActivateVoucher(int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher != null && DateTime.Now >= voucher.ActivationTime)
            {
                voucher.Status = Core.Enum.VoucherStatus.NotUsed; // Đặt trạng thái voucher phù hợp
                voucher.ReleaseDate = DateTime.Now;
                _context.Vouchers.Update(voucher);
            }
            await _context.SaveChangesAsync();
        }
    }
}
