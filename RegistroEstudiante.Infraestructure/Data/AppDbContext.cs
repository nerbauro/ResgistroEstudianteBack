using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Adapters.MySql.Entities;

namespace RegistroEstudiante.Adapters.MySql.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UsuarioEntity> Usuarios => Set<UsuarioEntity>();
    public DbSet<PermisoEntity> Permisos => Set<PermisoEntity>();
    public DbSet<MateriaEntity> Materias => Set<MateriaEntity>();
    public DbSet<MateriaProfesorEntity> MateriaProfesor => Set<MateriaProfesorEntity>();
    public DbSet<MateriaEstudianteEntity> Materiaestudiante => Set<MateriaEstudianteEntity>();
    public DbSet<RolEntity> Rol => Set<RolEntity>();
}
