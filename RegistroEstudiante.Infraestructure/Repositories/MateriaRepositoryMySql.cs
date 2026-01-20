using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Adapters.MySql.Entities;
using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Adapters.MySql.Repositories;

public class MateriaRepositoryMySql : IMateriaRepository
{
    private readonly AppDbContext _context;


    public MateriaRepositoryMySql(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MateriaDto>> ObtenerMateriaAsync()
    {
        List<MateriaDto> lstMateria = new List<MateriaDto>();
        try
        {
            var entity = await _context.Materias.ToListAsync();

            if (entity is not null)
            {
                lstMateria = entity.Select(x => new MateriaDto
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Creditos = x.Creditos
                }).ToList();
            }

            return lstMateria;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias. Error: {Ex.Message}");
        }
    }

    public async Task<MateriaDto> ObtenerMateriaPorIdAsync(int id)
    {
        try
        {
            var entity = await _context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null) throw new Exception("No se encontró información por el id enviado");

            return new MateriaDto
            {
                Codigo = entity.Codigo,
                Descripcion = entity.Descripcion,
                Creditos = entity.Creditos,
                Id = entity.Id
            };
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar la materia. Error: {Ex.Message}");
        }

    }

    public async Task<MateriaDto> ObtenerMateriaPorCodigoAsync(string codigo)
    {
        try
        {
            MateriaDto objMateria = new MateriaDto();

            var entity = await _context.Materias.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (entity is not null)
            {
                objMateria = new MateriaDto
                {
                    Codigo = entity.Codigo,
                    Descripcion = entity.Descripcion,
                    Creditos = entity.Creditos,
                    Id = entity.Id
                };
            }

            return objMateria;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar la materia. Error: {Ex.Message}");
        }

    }

    public async Task<MateriaDto> ObtenerMateriaPorCodigoIdAsync(string codigo, int id)
    {
        try
        {
            MateriaDto objMateria = new MateriaDto();

            var entity = await _context.Materias.FirstOrDefaultAsync(x => x.Codigo == codigo && x.Id != id);

            if (entity is not null)
            {
                objMateria = new MateriaDto
                {
                    Codigo = entity.Codigo,
                    Descripcion = entity.Descripcion,
                    Creditos = entity.Creditos,
                    Id = entity.Id
                };
            }

            return objMateria;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar la materia. Error: {Ex.Message}");
        }

    }

    public async Task<bool> CrearMateriaAsync(MateriaDto materia)
    {
        try
        {
            var entity = new MateriaEntity
            {
                Codigo = materia.Codigo,
                Descripcion = materia.Descripcion,
                Creditos = materia.Creditos
            };

            _context.Materias.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al intentar registrar la materia. Error: {Ex.Message}");
        }

    }

    public async Task<bool> EditarMateriaAsync(MateriaDto materia)
    {
        try
        {
            var entity = await _context.Materias
            .FirstOrDefaultAsync(x => x.Id == materia.Id);

            if (entity == null)
                return false;

            entity.Codigo = materia.Codigo;
            entity.Descripcion = materia.Descripcion;
            entity.Creditos = materia.Creditos;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al intentar actualizar la materia. Error: {Ex.Message}");
        }
    }

}
