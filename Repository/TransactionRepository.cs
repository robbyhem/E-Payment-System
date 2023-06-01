using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Repository
{
    public class TransactionRepository : ITransaction
    {
        private PaymentContext _eContext;

        public TransactionRepository(PaymentContext eContext)
        {
            _eContext = eContext;
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            _ = _eContext.AddAsync(transaction);
            await _eContext.SaveChangesAsync();
        }

        public async Task<Transaction> DeleteTransaction(int id)
        {
            if (id == 0) return null;
            var transactionId = await _eContext.Transactions.FindAsync(id);
            _eContext.Transactions.Remove(transactionId);
            return transactionId;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var transactionList = await _eContext.Transactions.ToListAsync();
            return transactionList;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            if (id == 0) return null;
            var transactionId = await _eContext.Transactions.FindAsync(id);
            return transactionId;
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            await _eContext.SaveChangesAsync();
        }
    }
}
