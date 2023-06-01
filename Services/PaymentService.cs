using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using Stripe;

namespace E_PaymentSystemAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayment _paymentRepo;
        private readonly ChargeService _chargeService;

        public PaymentService(IPayment paymentRepo, ChargeService chargeService, CustomerService customerService, TokenService tokenService)
        {
            _paymentRepo = paymentRepo;
            _chargeService = chargeService;
        }

        public async Task MakePayment(StripePayment stripePayment, CancellationToken ct)
        {
            var paymentDomain = new Payment()
            {
                Amount= stripePayment.Amount,
                Currency= stripePayment.Currency,
                Description= stripePayment.Description,
                CustomerId= stripePayment.CustomerId,
            };
            await _paymentRepo.MakePayment(paymentDomain);

            // Set the options for the payment we would like to create at Stripe
            ChargeCreateOptions paymentOptions = new ChargeCreateOptions
            {
                Amount = (long)stripePayment.Amount * 100,
                Currency = stripePayment.Currency,
                Description = stripePayment.Description,
                Customer = stripePayment.CustomerId
            };

            // Create the payment
            var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepo.GetPaymentById(id);
        }
    }
}
