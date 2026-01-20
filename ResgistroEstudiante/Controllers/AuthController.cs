using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RegistroEstudiante.Application.UseCases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistroEstudiante.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginUsuarioUseCase _login;
    private readonly IConfiguration _config;
    private readonly ObtenerPermisosUsuarioUseCase _permisos;


    public AuthController(LoginUsuarioUseCase login, IConfiguration config, ObtenerPermisosUsuarioUseCase permisos)
    {
        _login = login;
        _config = config;
        _permisos = permisos;
    }

    [HttpPost("login")]       
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var rolId = await _login.EjecutarAsync(request.Codigo, request.Clave);
        var permisos = await _permisos.EjecutarAsync(rolId);

        var claims = new List<Claim>();

        // Agregar todos los permisos como claim "permiso" y también como Role
        foreach (var permiso in permisos)
        {
            claims.Add(new Claim("permiso", permiso));
            claims.Add(new Claim(ClaimTypes.Role, permiso));
        }

        claims.Add(new Claim(ClaimTypes.Name, request.Codigo));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToUInt32(_config["Jwt:TokenLifetimeMinutes"])),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }


}

public record LoginRequest(string Codigo, string Clave);
