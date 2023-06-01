using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;

namespace E_PaymentSystemAPI.IServices
{
    public interface IPaymentService
    {
        Task MakePayment(StripePayment stripePayment, CancellationToken ct);
        Task<Payment> GetPaymentById(int id);
    }
}
