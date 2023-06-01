using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_PaymentSystemAPI.Repository
{
    public class UserRepository : IUser
    {
        private PaymentContext _eContext;
        //private readonly ILogger _logger;

        public UserRepository(PaymentContext context/*, ILogger logger*/)
        {
            _eContext = context;
            //_logger = logger;
        }

        public async Task RegisterUser(User user)
        {
            //_logger.LogInformation("User Registeration begins...");
            _ = _eContext.Users.AddAsync(user);
            await _eContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            if (id == 0) return null;
            var userId = await _eContext.Users.FindAsync(id);
            return userId;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _eContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserEmailExist(string email)
        {
            return await _eContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userList = await _eContext.Users.ToListAsync();
            return userList;
        }
    }
}
