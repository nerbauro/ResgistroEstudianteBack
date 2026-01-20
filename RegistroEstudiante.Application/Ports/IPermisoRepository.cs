namespace RegistroEstudiante.Application.Ports;

public interface IPermisoRepository
{
    Task<List<string>> ObtenerPermisosPorUsuarioAsync(int idRol);
}
