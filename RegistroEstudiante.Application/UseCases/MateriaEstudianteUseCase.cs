using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;
using Microsoft.Extensions.Configuration;

namespace RegistroEstudiante.Application.UseCases;

public class MateriaEstudianteUseCase
{
    private readonly IMateriaEstudianteRepository _repo;
    private readonly IMateriaProfesorRepository _repoP;
    private readonly int _maxMateriasEstudiante;

    public MateriaEstudianteUseCase(IMateriaEstudianteRepository repo, IConfiguration configuration, IMateriaProfesorRepository repoP)
    {
        _repo = repo;
        _maxMateriasEstudiante = configuration.GetValue<int>("CantidadMateriaEstudiante");
        _repoP = repoP;
    }


    public async Task<bool> CrearMateriaEstudiante (MateriaEstudianteDto materiaEstudiante)
    {
        List<MateriaEstudianteDto> objMateriaEstudianteExistente = await _repo.ObtenerMateriaEstudiantePorIdAsync(materiaEstudiante.IdEstudiante);
        
        if (objMateriaEstudianteExistente.Count >= _maxMateriasEstudiante)
            throw new Exception($"El Estudiante ya cuenta con {_maxMateriasEstudiante} materias. No se le pueden asignar mas materias al Estudiante");

        if(objMateriaEstudianteExistente.Count > 0)
        {
            var materiaExistente = objMateriaEstudianteExistente.Where(mp => mp.IdMateria == materiaEstudiante.IdMateria).FirstOrDefault();

            if(materiaExistente is not null)
                throw new Exception($"El Estudiante ya tiene asignada la materia. No se le puede asignar dos veces la misma materia al Estudiante");

            List<MateriaProfesorDto> lstMateriaProfesor = await _repo.ObtenerProfesorMateriaAsync(objMateriaEstudianteExistente);

            MateriaProfesorDto materiaProfesorNueva = await _repoP.ObtenerMateriaProfesorPorIdMateriaAsync(materiaEstudiante.IdMateria);

            var materiaProfesorExistente = lstMateriaProfesor.Where(mp => mp.IdProfesor == materiaProfesorNueva.IdProfesor).FirstOrDefault();
            
            if (materiaProfesorExistente is not null)
                throw new Exception($"El Estudiante ya tiene asignada una materia con el mismo profesor. No se le puede asignar dos veces el mismo profesor al Estudiante");
        }

        return await _repo.CrearMateriaEstudianteAsync(materiaEstudiante);
    }

    public async Task<List<MateriaEstudianteConProfesorDto>> ObtenerMateriaEstudianteConProfesorAsync(int idEstudiante)
    {
        return await _repo.ObtenerMateriaEstudianteConProfesorAsync(idEstudiante);
    }

    public async Task<List<MateriaEstudianteCompartidaDto>> ObtenerMateriaCompartidaAsync(int idEstudiante)
    {             
        return await _repo.ObtenerMateriaCompartidaAsync(idEstudiante);
    }

    

}
