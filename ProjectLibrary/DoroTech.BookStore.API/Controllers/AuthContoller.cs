using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoroTech.BookStore.API.Controllers.Models;

namespace DoroTech.BookStore.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// Realiza o Login do Administrador e retorna um token JWT que é usado para autenticação e acesso dos endpoints privados
    /// <param name="request">Credenciais do administrador</param>
    /// <returns>Token JWT</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var adminUser = _configuration["Jwt:AdminUser"];
        var adminPassword = _configuration["Jwt:AdminPassword"];
        // Váriaveis fixas para usuario e senha do admin, que estão contidas no appsettings.json

        if (request.Username != adminUser || request.Password != adminPassword)
            return Unauthorized("Usuário ou senha inválidos");
            /// retorno de erro em caso de senha ou usuario inválido, Login: Usuário: "admin",Senha: "123456"

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            //tempo de expiração de login
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
            //Geração do token se os parâmetros anteriores forem atendidos
        });
    }
}
