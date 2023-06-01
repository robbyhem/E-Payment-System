using System.ComponentModel.DataAnnotations;

namespace E_PaymentSystemAPI.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "This is a required field")]
        [StringLength(50, ErrorMessage = "Maximum character is 50")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(50, ErrorMessage = "Maximum character is 50")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [DataType(DataType.EmailAddress), StringLength(100, ErrorMessage = "Maximum character is 100")]
        public string Email { get; set; }


        [Required(ErrorMessage = "This is a required field")]
        [DataType(DataType.Password), StringLength(30, ErrorMessage = "Maximum character is 20"), MinLength(6, ErrorMessage = "Minimum character is 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(20, ErrorMessage = "Maximum character is 20")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "Maximum character is 200")]
        public string Address { get; set; }

        public StripeCard CreditCard { get; set; }
    }
}
