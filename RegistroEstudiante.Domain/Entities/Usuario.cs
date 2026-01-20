
using RegistroEstudiante.Domain.Exceptions;

namespace RegistroEstudiante.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Codigo { get; private set; } = null!;
    public string Nombre { get; private set; } = null!;
    public string ClaveHash { get; private set; } = null!;
    public string Email { get; private set; }
    public int RolId { get; private set; }
    public bool Activo { get; private set; }

    private Usuario() { }

    public Usuario(string codigo, string nombre, string clavePlano, string? email, int rolId)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new DomainException("El código de usuario es obligatorio");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es obligatorio");

        if (string.IsNullOrWhiteSpace(clavePlano) || clavePlano.Length < 8)
            throw new DomainException("La contraseña debe tener mínimo 8 caracteres");

        if (rolId <= 0)
            throw new DomainException("El rol es obligatorio");

        Codigo = codigo.Trim();
        Nombre = nombre.Trim();
        ClaveHash = clavePlano;
        Email = email;
        RolId = rolId;
        Activo = true;
    }



    public void CambiarClave(string nuevaClave)
    {
        if (string.IsNullOrWhiteSpace(nuevaClave) || nuevaClave.Length < 8)
            throw new DomainException("La nueva contraseña debe tener mínimo 8 caracteres");

        ClaveHash = BCrypt.Net.BCrypt.HashPassword(nuevaClave);
    }

    public void Desactivar() => Activo = false;
}
