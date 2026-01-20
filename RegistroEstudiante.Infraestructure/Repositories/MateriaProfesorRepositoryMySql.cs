using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Adapters.MySql.Entities;
using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Adapters.MySql.Repositories;

public class MateriaProfesorRepositoryMySql : IMateriaProfesorRepository
{
    private readonly AppDbContext _context;


    public MateriaProfesorRepositoryMySql(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MateriaProfesorDto>> ObtenerMateriaProfesorPorIdAsync(int idProfesor)
    {
        try
        {
            var entity = await _context.MateriaProfesor
           .Where(x => x.IdProfesor == idProfesor)
           .Select(x => new MateriaProfesorDto
           {
               Id = x.Id,
               IdProfesor = x.IdProfesor,
               IdMateria = x.IdMateria
           })
           .ToListAsync();

            return entity;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias del profesor. Error: {Ex.Message}");
        }

    }

    public async Task<MateriaProfesorDto> ObtenerMateriaProfesorPorIdMateriaAsync(int idMateria)
    {
        try
        {
            var entity = await _context.MateriaProfesor
           .FirstOrDefaultAsync(x => x.IdMateria == idMateria);

            return new MateriaProfesorDto
            {
                IdMateria = entity.IdMateria,
                IdProfesor = entity.IdProfesor
            };
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias del profesor. Error: {Ex.Message}");
        }

    }

    public async Task<bool> CrearMateriaProfesorAsync(MateriaProfesorDto materiaProfesor)
    {
        try
        {
            var entity = new MateriaProfesorEntity
            {
                IdMateria = materiaProfesor.IdMateria,
                IdProfesor = materiaProfesor.IdProfesor
            };

            _context.MateriaProfesor.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al intentar registrar la materia al profesor. Error: {Ex.Message}");
        }

    }


}
