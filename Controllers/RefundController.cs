using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private IRefund _refundRepo;

        public RefundController(IRefund refundRepo)
        {
            _refundRepo = refundRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRefund([FromBody] Refund refund)
        {
            if (refund == null) return BadRequest("Refund is null");
            await _refundRepo.CreateRefund(refund);
            return CreatedAtAction("GetAllRefunds", new {Id = refund.Id}, refund);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefundById(int id)
        {
            if (id == 0) return BadRequest("Refund Id is invalid");
            var refundIdCT = await _refundRepo.GetRefundById(id);
            return Ok(refundIdCT);
        }
    }
}
