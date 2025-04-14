using FinanceManager.API.Application.Interfaces.Services;
using FinanceManager.Shared.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Presentation.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var result = await _userService.InsertAsync(user);
            return Ok(result);
        }
    }
}
