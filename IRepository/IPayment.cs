using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;

namespace E_PaymentSystemAPI.IRepository
{
    public interface IPayment
    {
        Task MakePayment(Payment payment);
        Task<Payment> GetPaymentById(int id);
    }
}
