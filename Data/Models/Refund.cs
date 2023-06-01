using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Data.Models
{
    public class Refund
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [ForeignKey(nameof(Payment))]
        public int PaymentId { get; set; }

        [Required]
        [Precision(10, 2)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(250, ErrorMessage = "Maximum character is 250")]
        public string Reason { get; set; }

        [Required]
        public bool Approved { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(10, ErrorMessage = "Maximum character is 10")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
    }
}
