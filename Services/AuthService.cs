using E_PaymentSystemAPI.Auth;
using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_PaymentSystemAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUser _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration configuration, IUser userRepo)
        {
            _config = configuration;
            _userRepo = userRepo;
        }

        public async Task<UserLogin> VerifyToken(UserLogin userLogin, string email)
        {
            var user = await _userRepo.GetUserByEmail(userLogin.Email);

            if (user == null || user.Password != userLogin.Password)
                return null;
            string tokenString = GenerateToken(user.Email);
            return new UserLogin { Email = tokenString };
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),  // |AddDays(7),|
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
