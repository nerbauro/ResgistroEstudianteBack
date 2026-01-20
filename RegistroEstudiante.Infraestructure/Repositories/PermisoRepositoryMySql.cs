using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Application.Ports;

namespace RegistroEstudiante.Adapters.MySql.Repositories;

public class PermisoRepositoryMySql : IPermisoRepository
{
    private readonly AppDbContext _context;

    public PermisoRepositoryMySql(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> ObtenerPermisosPorUsuarioAsync(int idRol)
    {
        try
        {
            return await _context.Permisos
                .Where(p => p.RolId == idRol)
                .Select(p => $"{p.Controlador}.{p.Pagina}")
                .ToListAsync();
        }
        catch(Exception Ex)
        {
            throw new Exception($"Error: {Ex.Message}");
        }
    }
}
