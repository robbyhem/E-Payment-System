using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_PaymentSystemAPI.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        //[DataType(DataType.Date)]
        public DateTime DateRegistered { get; set; } = DateTime.Now;
    }

}
