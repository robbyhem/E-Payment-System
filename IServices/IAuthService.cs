using E_PaymentSystemAPI.Auth;
using Stripe;

namespace E_PaymentSystemAPI.IServices
{
    public interface IAuthService
    {
        Task<UserLogin> VerifyToken(UserLogin userLogin, string email);
    }
}
