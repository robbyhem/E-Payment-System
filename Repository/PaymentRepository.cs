using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Repository
{
    public class PaymentRepository : IPayment
    {
        private PaymentContext _eContext;

        public PaymentRepository(PaymentContext eContext)
        {
            _eContext = eContext;
        }
    
        public async Task MakePayment(Payment payment)
        {
            _ = _eContext.AddAsync(payment);
            await _eContext.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            if (id == 0) return null;
            var paymentId = await _eContext.Payments.FindAsync(id);
            return paymentId;
        }
    }
}
