using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiante.Application.UseCases;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Api.Controllers;

[ApiController]
[Route("api/materia")]
public class MateriaController : ControllerBase
{
    private readonly MateriaUseCase _useCase;

    public MateriaController(MateriaUseCase useCase)
    {
        _useCase = useCase;
    }

    [Authorize(Policy = "Permiso", Roles = "Materia.Consultar")]
    [HttpGet("Consultar")]
    public async Task<IActionResult> Consultar()
    {
        return Ok(await _useCase.ObtenerMateriaAsync());
    }

    [Authorize(Policy = "Permiso", Roles = "Materia.ConsultarPorCodigo")]
    [HttpGet("ConsultarPorCodigo")]
    public async Task<IActionResult> ConsultarPorCodigo(string codigo)
    {
        return Ok(await _useCase.ObtenerMateriaPorCodigoAsync(codigo));
    }

    [Authorize(Policy = "Permiso", Roles = "Materia.Crear")]
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear(MateriaDto request)
    {
        return Ok(await _useCase.CrearMateria(request));
    }

    [Authorize(Policy = "Permiso", Roles = "Materia.Editar")]
    [HttpPost("Editar")]
    public async Task<IActionResult> Editar(MateriaDto request)
    {
        return Ok(await _useCase.EditarMateriaAsync(request));
    }
}
