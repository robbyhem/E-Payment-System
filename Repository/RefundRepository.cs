using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Repository
{
    public class RefundRepository : IRefund
    {
        private PaymentContext _eContext;

        public RefundRepository(PaymentContext eContext)
        {
            _eContext = eContext;
        }

        public async Task CreateRefund(Refund refund)
        {
            _ = _eContext.AddAsync(refund);
            await _eContext.SaveChangesAsync();
        }

        public async Task<Refund> DeleteRefund(int id)
        {
            if (id == 0) return null;
            var refundId = await _eContext.Refunds.FindAsync(id);
            _eContext.Refunds.Remove(refundId);
            return refundId;
        }

        public async Task<IEnumerable<Refund>> GetAllRefunds()
        {
            var refundList = await _eContext.Refunds.ToListAsync();
            return refundList;
        }

        public async Task<Refund> GetRefundById(int id)
        {
            if (id == 0) return null;
            var refundId = await _eContext.Refunds.FindAsync(id);
            return refundId;
        }

        public async Task UpdateRefund(Refund refund)
        {
            if (refund == null) throw new ArgumentNullException(nameof(Refund));
            await _eContext.SaveChangesAsync();
        }
    }
}
