using AutoMapper;
using E_PaymentSystemAPI.Auth;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using E_PaymentSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IAuthService authService, IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous, HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDTO createUserDTO, CancellationToken ct)
        {
            try
            {
                //_logger.LogInformation("Executing Register User...");
                var userCt = new User();
                await _userService.RegisterUser(createUserDTO, ct);
                return CreatedAtAction("GetUserById", new { id = userCt.Id }, createUserDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous, HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLogin userLogin)
        {
            //_logger.LogInformation("Executing User Loggin...");
            try
            {
                var user = await _userService.GetUserByEmail(userLogin.Email);

                if (user == null)
                {
                    return BadRequest("Invalid email or password");
                }

                if (user.Password != userLogin.Password)
                {
                    return BadRequest("Invalid email or password");
                }

                var token = await _authService.VerifyToken(userLogin, user.Email);
                if (token == null)
                    return Unauthorized();
                return Ok(new TokenResponse { Token = token.Email });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id == 0) return BadRequest("User Id is invalid");
            var userIdCT = await _userService.GetUserById(id);
            return Ok(userIdCT);
        }

        [/*Authorize,*/ HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userListCT = await _userService.GetAllUsers();
            return Ok(userListCT);
        }
    }
}
