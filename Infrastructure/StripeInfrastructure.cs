using E_PaymentSystemAPI.IServices;
using E_PaymentSystemAPI.Services;
using Stripe;

namespace E_PaymentSystemAPI.Infrastructure
{
    public static class StripeInfrastructure
    {
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("Stripe:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPaymentService, PaymentService>();
        }
    }
}
