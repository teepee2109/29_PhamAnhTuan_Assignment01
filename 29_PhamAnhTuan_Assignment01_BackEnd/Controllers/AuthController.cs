using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController, Route("api/auth")]
public sealed class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken ct)
    {
        var result = await service.LoginAsync(request, ct);
        return result is null ? Unauthorized("Email hoặc password không đúng.") : Ok(result);
    }
}
