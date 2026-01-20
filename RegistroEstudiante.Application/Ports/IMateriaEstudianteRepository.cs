using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Application.Ports;

public interface IMateriaEstudianteRepository
{
    Task<List<MateriaEstudianteDto>> ObtenerMateriaEstudiantePorIdAsync(int idEstudiante);
    Task<List<MateriaProfesorDto>> ObtenerProfesorMateriaAsync(List<MateriaEstudianteDto> lstMateriasEstudiante);
    Task<bool> CrearMateriaEstudianteAsync(MateriaEstudianteDto materiaEstudiante);
    Task<List<MateriaEstudianteConProfesorDto>> ObtenerMateriaEstudianteConProfesorAsync(int idEstudiante);
    Task<List<MateriaEstudianteCompartidaDto>> ObtenerMateriaCompartidaAsync(int idEstudiante);
}
