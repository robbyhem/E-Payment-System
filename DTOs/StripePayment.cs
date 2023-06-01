namespace E_PaymentSystemAPI.DTOs
{
    public class StripePayment
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
    }
}
