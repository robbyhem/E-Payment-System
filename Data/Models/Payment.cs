using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_PaymentSystemAPI.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        
        //[ForeignKey(nameof(User))]
        //public int UserId { get; set; }
        
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }

        
    }
}
