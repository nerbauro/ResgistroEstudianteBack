using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Adapters.MySql.Entities;
using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;

namespace RegistroEstudiante.Adapters.MySql.Repositories;

public class MateriaEstudianteRepositoryMySql : IMateriaEstudianteRepository
{
    private readonly AppDbContext _context;


    public MateriaEstudianteRepositoryMySql(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MateriaEstudianteDto>> ObtenerMateriaEstudiantePorIdAsync(int idEstudiante)
    {
        try
        {
            var entity = await _context.Materiaestudiante
           .Where(x => x.IdEstudiante == idEstudiante)
           .Select(x => new MateriaEstudianteDto
           {
               Id = x.Id,
               IdEstudiante = x.IdEstudiante,
               IdMateria = x.IdMateria
           })
           .ToListAsync();

            return entity;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias del estudiante. Error: {Ex.Message}");
        }

    }

    public async Task<List<MateriaProfesorDto>> ObtenerProfesorMateriaAsync(List<MateriaEstudianteDto> lstMateriasEstudiante)
    {
        try
        {
            var idsMateria = lstMateriasEstudiante
                .Select(m => m.IdMateria)
                .Distinct()
                .ToList();

            var entity = await _context.MateriaProfesor
                .Where(x => idsMateria.Contains(x.IdMateria))
                .Select(x => new MateriaProfesorDto
                {
                    Id = x.Id,
                    IdMateria = x.IdMateria,
                    IdProfesor = x.IdProfesor
                })
                .ToListAsync();

            return entity;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias del estudiante. Error: {Ex.Message}");
        }
    }

    public async Task<List<MateriaEstudianteCompartidaDto>> ObtenerMateriaCompartidaAsync(int idEstudiante)
    {
        try
        {
            var idsMateria = await _context.Materiaestudiante
                .Where(me => me.IdEstudiante == idEstudiante)
                .Select(me => me.IdMateria)
                .Distinct()
                .ToListAsync();

            var entity = await _context.Materiaestudiante
                .Where(me => idsMateria.Contains(me.IdMateria) && me.IdEstudiante != idEstudiante)
                .Join(_context.Materias,
                      me => me.IdMateria,
                      m => m.Id,
                      (me, m) => new { me, m })
                .Join(_context.Usuarios,
                      mm => mm.me.IdEstudiante,
                      u => u.Id,
                      (mm, u) => new MateriaEstudianteCompartidaDto
                      {
                          IdMateria = mm.m.Id,
                          nombreMateria = mm.m.Descripcion,
                          nombreEstudiante = u.Nombre
                      })
                .ToListAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias compartidas. Error: {ex.Message}");
        }
    }

    public async Task<List<MateriaEstudianteConProfesorDto>> ObtenerMateriaEstudianteConProfesorAsync(int idEstudiante)
    {
        try
        {
            var entity = await _context.Materiaestudiante
                .Where(me => me.IdEstudiante == idEstudiante)
                .Join(_context.Materias,
                      me => me.IdMateria,
                      m => m.Id,
                      (me, m) => new { me, m })
                .Join(_context.MateriaProfesor,
                      mm => mm.m.Id,
                      mp => mp.IdMateria,
                      (mm, mp) => new { mm, mp })
                .Join(_context.Usuarios,
                      mmp => mmp.mp.IdProfesor,
                      u => u.Id,
                      (mmp, u) => new MateriaEstudianteConProfesorDto
                      {
                          IdMateria = mmp.mm.m.Id,
                          NombreMateria = mmp.mm.m.Descripcion,
                          NombreProfesor = u.Nombre
                      })
                .ToListAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocurrió un error al consultar las materias y profesores. Error: {ex.Message}");
        }
    }


    public async Task<bool> CrearMateriaEstudianteAsync(MateriaEstudianteDto materiaEstudiante)
    {
        try
        {
            var entity = new MateriaEstudianteEntity
            {
                IdMateria = materiaEstudiante.IdMateria,
                IdEstudiante = materiaEstudiante.IdEstudiante
            };

            _context.Materiaestudiante.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception Ex)
        {
            throw new Exception($"Ocurrió un error al intentar registrar la materia al Estudiante. Error: {Ex.Message}");
        }

    }


}
