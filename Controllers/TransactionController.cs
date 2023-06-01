using E_PaymentSystemAPI.Data.Models;
using E_PaymentSystemAPI.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_PaymentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransaction _transactionRepo;

        public TransactionController(ITransaction transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest("Transaction is null");
            await _transactionRepo.CreateTransaction(transaction);
            return CreatedAtAction("GetAllTransactions", new { id = transaction.Id }, transaction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            if (id == 0) return BadRequest("Transaction Id is invalid");
            var transactionIdCT = await _transactionRepo.GetTransactionById(id);
            return Ok(transactionIdCT);
        }
    }
}
