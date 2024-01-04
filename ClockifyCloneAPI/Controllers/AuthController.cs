using ClockifyCloneAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    [HttpGet("login")]
    public IActionResult Login(string email, string password)
    {
        try
        {
            var claimsPrincipal = _authService.GetEmailClaimsPrincipal(email, password);
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