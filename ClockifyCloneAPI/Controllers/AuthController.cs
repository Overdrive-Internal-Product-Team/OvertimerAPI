using ClockifyCloneAPI.Models.Auth;
using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockifyCloneAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(PostLoginRequest request)
    {
        Console.WriteLine("Entrou");
        try
        {
            var claimsPrincipal = _authService.GetEmailClaimsPrincipal(request.Email, request.Password);
            return SignIn(claimsPrincipal);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Credenciais inválidas");
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }      
    }
  

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var user = await _authService.GetAuthUserInfos(User);
            return Ok(user);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Usuário não autorizado!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }     
    }
}