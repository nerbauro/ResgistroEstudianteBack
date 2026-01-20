using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Adapters.MySql.Entities;
using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;
using RegistroEstudiante.Domain.Entities;

namespace RegistroEstudiante.Adapters.MySql.Repositories;

public class UsuarioRepositoryMySql : IUsuarioRepository
{
    private readonly AppDbContext _context;


    public UsuarioRepositoryMySql(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<UsuarioDto>> ObtenerUsuariosAsync()
    {
        return await (from u in _context.Usuarios
                      join r in _context.Rol on u.RolId equals r.Id
                      select new UsuarioDto
                      {
                          Id = u.Id,
                          Codigo = u.Codigo,
                          Nombre = u.Nombre,
                          Clave = u.ClaveHash,
                          Email = u.Email,
                          RolId = u.RolId,
                          Activo = u.Activo,
                          NombreRol = r.Nombre
                      }).ToListAsync();
    }

    public async Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int idUsuario)
    {
        return await _context.Usuarios
            .Where(usu => usu.Id == idUsuario)
           .Select(entity => new UsuarioDto
           {
               Id = entity.Id,
               Codigo = entity.Codigo,
               Nombre = entity.Nombre,
               Clave = entity.ClaveHash,
               Email = entity.Email,
               RolId = entity.RolId,
               Activo = entity.Activo
           })
           .FirstOrDefaultAsync();
    }

    public async Task<UsuarioDto?> ObtenerPorCodigoAsync(string codigo ,string clave)
    {
        var entity = await _context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
        if (entity == null) return null;

        return new UsuarioDto { 
            Codigo = entity.Codigo, 
            Nombre = entity.Nombre, 
            Clave = entity.ClaveHash, 
            Email = entity.Email, 
            RolId = entity.RolId,
            Activo = entity.Activo};
    }

    public async Task<Usuario?> ObtenerUsuarioPorCodigoAsync(string codigo)
    {
        var entity = await _context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
        if (entity == null) return null;

        return new Usuario(entity.Codigo, entity.Nombre, "", entity.Email, entity.RolId);
    }

    public async Task<bool> CrearAsync(Usuario usuario)
    {
        var entity = new UsuarioEntity
        {
            Codigo = usuario.Codigo,
            Nombre = usuario.Nombre,
            ClaveHash = usuario.ClaveHash,
            Email = usuario.Email,
            RolId = usuario.RolId,
            Activo = usuario.Activo
        };

        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EditarAsync(UsuarioDto usuario)
    {
        var entity = await _context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == usuario.Codigo);
        if (entity == null) return false;

        entity.Nombre = usuario.Nombre;
        entity.ClaveHash = usuario.Clave;
        entity.Email = usuario.Email;
        entity.RolId = usuario.RolId;
        entity.Activo = usuario.Activo;

        await _context.SaveChangesAsync();

        return true;
    }


    public ICollection<PermisoEntity> Permisos { get; set; } = new List<PermisoEntity>();
}
