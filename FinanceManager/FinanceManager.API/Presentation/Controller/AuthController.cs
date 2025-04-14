using FinanceManager.API.Application.Interfaces.Authentication;
using FinanceManager.API.Application.Interfaces.Services;
using FinanceManager.Shared.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Presentation.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserService _userService;

        public AuthController(IJwtTokenGenerator tokenGenerator, IUserService userService)
        {
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetByLoginInfoAsync(request.Email, request.Password);
            var token = _tokenGenerator.GenerateToken(user);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok("Hello, Admin!");
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("admin-policy-secured")]
        public IActionResult AdminPolicySecuredEndpoint()
        {
            return Ok("Secured with Admin Policy");
        }

        [Authorize(Policy = "StandardPolicy")]
        [HttpGet("user-policy-secured")]
        public IActionResult UserPolicySecuredEndpoint()
        {
            return Ok("Secured with User Policy");
        }
    }
}