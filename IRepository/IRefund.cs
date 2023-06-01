using E_PaymentSystemAPI.Data.Models;

namespace E_PaymentSystemAPI.IRepository
{
    public interface IRefund
    {
        Task CreateRefund(Refund refund);
        Task UpdateRefund(Refund refund);
        Task<IEnumerable<Refund>> GetAllRefunds();
        Task<Refund> GetRefundById(int id);
        Task<Refund> DeleteRefund(int id);
    }
}
