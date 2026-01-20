using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiante.Adapters.MySql.Entities;

[Table("usuario")]
public class UsuarioEntity
{
    [Key]
    [Column("Usu_Id")]
    public int Id { get; set; }

    [Column("Usu_Codigo")]
    public string Codigo { get; set; } = null!;

    [Column("Usu_Nombre")]
    public string Nombre { get; set; } = null!;

    [Column("Usu_Clave")]
    public string ClaveHash { get; set; } = null!;

    [Column("Usu_Email")]
    public string? Email { get; set; }

    [Column("Rol_Id")]
    public int RolId { get; set; }

    [Column("Usu_Activo")]
    public bool Activo { get; set; }
}
