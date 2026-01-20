using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Application.Ports;

public interface IMateriaProfesorRepository
{
    Task<List<MateriaProfesorDto>> ObtenerMateriaProfesorPorIdAsync(int idProfesor);
    Task<MateriaProfesorDto> ObtenerMateriaProfesorPorIdMateriaAsync(int idMateria);
    Task<bool> CrearMateriaProfesorAsync(MateriaProfesorDto materiaProfesor);
}
