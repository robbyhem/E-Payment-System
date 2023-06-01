using AutoMapper;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using E_PaymentSystemAPI.Repository;
using Stripe;
using System.Runtime.CompilerServices;

namespace E_PaymentSystemAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _userRepo;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUser userRepo, CustomerService customerService, TokenService tokenService, ILogger<UserService> logger)
        {
            _userRepo = userRepo;
            _customerService = customerService;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task RegisterUser(CreateUserDTO createUserDTO, CancellationToken ct)
        {
            _logger.LogInformation("Executing RegisterUser Method...");

            try
            {
                // Add User to the Database
                var userDomain = new User()
                {
                    FirstName = createUserDTO.FirstName,
                    LastName = createUserDTO.LastName,
                    Email = createUserDTO.Email,
                    Password = createUserDTO.Password,
                    PhoneNumber = createUserDTO.PhoneNumber,
                    Address = createUserDTO.Address,
                };

                if (await _userRepo.UserEmailExist(userDomain.Email))
                {
                    throw new InvalidOperationException("Email address already exists");
                }
                await _userRepo.RegisterUser(userDomain);

                // Set Stripe Token options based on customer data
                TokenCreateOptions tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name = $"{createUserDTO.FirstName} {createUserDTO.LastName}",
                        Number = createUserDTO.CreditCard.CardNumber,
                        ExpYear = createUserDTO.CreditCard.ExpirationYear,
                        ExpMonth = createUserDTO.CreditCard.ExpirationMonth,
                        Cvc = createUserDTO.CreditCard.Cvc
                    }
                };

                // Create new Stripe Token
                Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);

                // Set Customer options using
                CustomerCreateOptions customerOptions = new CustomerCreateOptions
                {
                    Name = $"{createUserDTO.FirstName} {createUserDTO.LastName}",
                    Email = createUserDTO.Email,
                    Source = stripeToken.Id
                };

                // Create customer at Stripe
                Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepo.GetUserByEmail(email);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.GetAllUsers();
        }
    }
}
