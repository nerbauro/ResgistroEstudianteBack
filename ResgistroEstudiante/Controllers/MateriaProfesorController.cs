using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistroEstudiante.Application.UseCases;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Api.Controllers;

[ApiController]
[Route("api/materiaProfesor")]
public class MateriaProfesorController : ControllerBase
{
    private readonly MateriaProfesorUseCase _useCase;

    public MateriaProfesorController(MateriaProfesorUseCase useCase)
    {
        _useCase = useCase;
    }

    
    [Authorize(Policy = "Permiso", Roles = "MateriaProfesor.Crear")]
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear(MateriaProfesorDto request)
    {
        return Ok(await _useCase.CrearMateriaProfesor(request));
    }

}
