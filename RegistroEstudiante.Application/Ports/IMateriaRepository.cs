using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Application.Ports;

public interface IMateriaRepository
{
    Task<List<MateriaDto>> ObtenerMateriaAsync();
    Task<MateriaDto> ObtenerMateriaPorIdAsync(int id);
    Task<MateriaDto> ObtenerMateriaPorCodigoAsync(string codigo);
    Task<MateriaDto> ObtenerMateriaPorCodigoIdAsync(string codigo, int id);
    Task<bool> CrearMateriaAsync(MateriaDto materia);
    Task<bool> EditarMateriaAsync(MateriaDto materia);
}
