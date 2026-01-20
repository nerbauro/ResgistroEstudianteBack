using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiante.Application.UseCases;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly CrearUsuarioUseCase _useCase;

    public UsuariosController(CrearUsuarioUseCase useCase)
    {
        _useCase = useCase;
    }

    [Authorize(Policy = "Permiso", Roles = "Usuario.Consultar")]
    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        return Ok(await _useCase.ObtenerUsuariosAsync());
    }

    [Authorize(Policy = "Permiso", Roles = "Usuario.ConsultarUsuarioPorId")]
    [HttpGet("ConsultarUsuarioPorId")]
    public async Task<IActionResult> ConsultarUsuarioPorId(int idUsuario)
    {
        return Ok(await _useCase.ObtenerUsuarioPorIdAsync(idUsuario));
    }             

    [Authorize(Policy = "Permiso", Roles = "Usuario.Crear")]
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear(UsuarioDto request)
    {
        return Ok(await _useCase.EjecutarAsync(request));
    }

    [Authorize(Policy = "Permiso", Roles = "Usuario.Editar")]
    [HttpPost("Editar")]
    public async Task<IActionResult> Editar(UsuarioDto request)
    {
        return Ok(await _useCase.EditarAsync(request));
    }
}
