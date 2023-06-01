using E_PaymentSystemAPI.Data.Models;

namespace E_PaymentSystemAPI.IRepository
{
    public interface ITransaction
    {
        Task CreateTransaction(Transaction transction);
        Task UpdateTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> DeleteTransaction(int id);
    }
}
