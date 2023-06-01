using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;

namespace E_PaymentSystemAPI.IServices
{
    public interface IUserService
    {
        Task RegisterUser(CreateUserDTO createUserDTO, CancellationToken ct);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
