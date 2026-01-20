using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiante.Application.UseCases;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Api.Controllers;

[ApiController]
[Route("api/materiaEstudiante")]
public class MateriaEstudianteController : ControllerBase
{
    private readonly MateriaEstudianteUseCase _useCase;

    public MateriaEstudianteController(MateriaEstudianteUseCase useCase)
    {
        _useCase = useCase;
    }

    
    [Authorize(Policy = "Permiso", Roles = "MateriaEstudiante.Crear")]
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear(MateriaEstudianteDto request)
    {
        return Ok(await _useCase.CrearMateriaEstudiante(request));
    }

    [Authorize(Policy = "Permiso", Roles = "MateriaEstudiante.ObtenerMateriaCompartida")]
    [HttpGet("ObtenerMateriaCompartida")]
    public async Task<IActionResult> ObtenerMateriaCompartidaAsync(int idEstudiante)
    {
        return Ok(await _useCase.ObtenerMateriaCompartidaAsync(idEstudiante));
    }

    [Authorize(Policy = "Permiso", Roles = "MateriaEstudiante.ObtenerMateriaEstudianteConProfesor")]
    [HttpGet("ObtenerMateriaEstudianteConProfesor")]
    public async Task<IActionResult> ObtenerMateriaEstudianteConProfesorAsync(int idEstudiante)
    {
        return Ok(await _useCase.ObtenerMateriaEstudianteConProfesorAsync(idEstudiante));
    }

    

}
