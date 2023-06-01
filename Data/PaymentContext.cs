using E_PaymentSystemAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Data
{
    public class PaymentContext : /*Identity*/ DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
