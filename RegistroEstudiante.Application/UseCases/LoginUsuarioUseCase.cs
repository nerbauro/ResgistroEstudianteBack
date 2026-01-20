using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Exceptions;

namespace RegistroEstudiante.Application.UseCases;

public class LoginUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public LoginUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> EjecutarAsync(string codigo, string clave)
    {
        var usuario = await _repo.ObtenerPorCodigoAsync(codigo, clave);

        if (usuario == null)
            throw new DomainException("Usuario o contraseña incorrectos");
              

        if (!ValidarClave(clave, usuario.Clave))
            throw new DomainException("Usuario o contraseña incorrectos");

        if (!usuario.Activo)
            throw new DomainException("Usuario inactivo");

        return usuario.RolId;
    }


    private bool ValidarClave(string clavePlano, string claveEncriptada)
        => BCrypt.Net.BCrypt.Verify(clavePlano, claveEncriptada);
}
