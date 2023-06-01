using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_PaymentSystemAPI.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [ForeignKey(nameof(Transaction))]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [ForeignKey(nameof(Payment))]
        public int PaymentId { get; set; }

        [Required]
        [Precision(10, 2)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(50, ErrorMessage = "Maximum character is 30")]
        public string Type { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(50, ErrorMessage = "Maximum character is 30")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
    }
}
