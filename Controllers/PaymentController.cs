using AutoMapper;
using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.DTOs;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using E_PaymentSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpPost("makePayment")]
        public async Task<IActionResult> MakePayment([FromBody] StripePayment stripePayment, CancellationToken ct)
        {
            try
            {
                var userCt = new User();
                await _paymentService.MakePayment(stripePayment, ct);
                return CreatedAtAction("GetPaymentById", new { id = userCt.Id }, stripePayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            if (id == 0) return BadRequest("Payment Id is invalid");
            var paymentIdCT = await _paymentService.GetPaymentById(id);
            return Ok(paymentIdCT);
        }
    }
}
