using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Domain.Dtos;
using RegistroEstudiante.Domain.Entities;
using System.Runtime.CompilerServices;

namespace RegistroEstudiante.Application.UseCases;

public class CrearUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public CrearUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<UsuarioDto>> ObtenerUsuariosAsync()
    {
        return await _repo.ObtenerUsuariosAsync();          
    }

    public async Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int idUsuario)
    {
        return await _repo.ObtenerUsuarioPorIdAsync(idUsuario);
    }                

    public async Task<bool> EjecutarAsync(UsuarioDto request)
    {
        var existente = await _repo.ObtenerPorCodigoAsync(request.Codigo, request.Clave);
        if (existente != null)
            throw new Exception("El usuario ya existe");
        
        string claveHash = BCrypt.Net.BCrypt.HashPassword(request.Clave);

        var usuario = new Usuario(request.Codigo, request.Nombre, claveHash, request.Email, request.RolId);
        return await _repo.CrearAsync(usuario);
    }

    public async Task<bool> EditarAsync(UsuarioDto request)
    {
        request.Clave = BCrypt.Net.BCrypt.HashPassword(request.Clave);

        return await _repo.EditarAsync(request);
    }
}
