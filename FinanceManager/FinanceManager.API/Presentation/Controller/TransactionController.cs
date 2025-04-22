using FinanceManager.API.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Presentation.Controller
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [Authorize(Policy = "StandardPolicy")]
        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetByUserIdAsync(long userId)
        {
            var result = await _transactionService.GetByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
