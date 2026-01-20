using RegistroEstudiante.Domain.Dtos;
using RegistroEstudiante.Domain.Entities;

namespace RegistroEstudiante.Application.Ports;

public interface IUsuarioRepository
{
    Task<List<UsuarioDto>> ObtenerUsuariosAsync();
    Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int idUsuario);
    Task<UsuarioDto?> ObtenerPorCodigoAsync(string codigo, string clave);
    Task<Usuario?> ObtenerUsuarioPorCodigoAsync(string codigo);
    Task<bool> CrearAsync(Usuario usuario);
    Task<bool> EditarAsync(UsuarioDto usuario);
}
