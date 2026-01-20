using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Application.UseCases;

public class MateriaUseCase
{
    private readonly IMateriaRepository _repo;

    public MateriaUseCase(IMateriaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MateriaDto>> ObtenerMateriaAsync()
    {
         return await _repo.ObtenerMateriaAsync();
    }

    public async Task<MateriaDto> ObtenerMateriaPorCodigoAsync(string codigo)
    {
        return await _repo.ObtenerMateriaPorCodigoAsync(codigo);
    }

    public async Task<bool> CrearMateria (MateriaDto materia)
    {
        MateriaDto objMateriaExistente = await _repo.ObtenerMateriaPorCodigoAsync(materia.Codigo);
        
        if (objMateriaExistente is not null && !string.IsNullOrEmpty(objMateriaExistente.Codigo))
            throw new Exception("Ya existe una materia con el mismo código");

        return await _repo.CrearMateriaAsync(materia);
    }

    public async Task<bool> EditarMateriaAsync(MateriaDto materia)
    {
        var existente = await _repo.ObtenerMateriaPorIdAsync(materia.Id);
        if (existente is null)
            throw new Exception("La materia no existe");

        MateriaDto objMateriaExistente = await _repo.ObtenerMateriaPorCodigoIdAsync(materia.Codigo, materia.Id);

        if (objMateriaExistente is not null && !string.IsNullOrEmpty(objMateriaExistente.Codigo))
            throw new Exception("Ya existe una materia con el mismo código");

        return await _repo.EditarMateriaAsync(materia);
    }
}
