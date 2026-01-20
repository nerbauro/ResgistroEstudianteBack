using RegistroEstudiante.Application.Ports;

namespace RegistroEstudiante.Application.UseCases;

public class ObtenerPermisosUsuarioUseCase
{
    private readonly IPermisoRepository _repo;

    public ObtenerPermisosUsuarioUseCase(IPermisoRepository repo)
    {
        _repo = repo;
    }

    public Task<List<string>> EjecutarAsync(int idRol)
        => _repo.ObtenerPermisosPorUsuarioAsync(idRol);
}
