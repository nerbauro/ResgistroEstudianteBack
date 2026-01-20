using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace RegistroEstudiante.Application.UseCases;

public class MateriaProfesorUseCase
{
    private readonly IMateriaProfesorRepository _repo;
    private readonly int _maxMateriasProfesor;

    public MateriaProfesorUseCase(IMateriaProfesorRepository repo, IConfiguration configuration)
    {
        _repo = repo;
        _maxMateriasProfesor = configuration.GetValue<int>("CantidadMateriaProfesor");
    }


    public async Task<bool> CrearMateriaProfesor (MateriaProfesorDto materiaProfesor)
    {
        List<MateriaProfesorDto> objMateriaProfesorExistente = await _repo.ObtenerMateriaProfesorPorIdAsync(materiaProfesor.IdProfesor);
        
        if (objMateriaProfesorExistente.Count >= _maxMateriasProfesor)
            throw new Exception($"El profesor ya cuenta con {_maxMateriasProfesor} materias. No se le pueden asignar mas materias al profesor");

        if(objMateriaProfesorExistente.Count > 0)
        {
            var materiaExistente = objMateriaProfesorExistente.Where(mp => mp.IdMateria == materiaProfesor.IdMateria).FirstOrDefault();

            if(materiaExistente is not null)
                throw new Exception($"El profesor ya tiene asignada la materia. No se le puede asignar dos veces la misma materia al profesor");
        }

        return await _repo.CrearMateriaProfesorAsync(materiaProfesor);
    }

}
