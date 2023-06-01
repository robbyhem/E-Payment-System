using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;

namespace E_PaymentSystemAPI.IRepository
{
    public interface IUser
    {
        Task RegisterUser(User user);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<bool> UserEmailExist(string email);
    }
}
