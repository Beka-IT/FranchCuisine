using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly JwtAuthService _authService;

    public AuthController(JwtAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(User user)
    {
        var token = await _authService.SignInAsync(user);
        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(User user)
    {
        await _authService.SignUpAsync(user);

        return Ok();
    }
}