using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiante.Adapters.MySql.Entities;

[Table("permiso")]
public class PermisoEntity
{
    [Key]
    [Column("Per_Id")]
    public int Id { get; set; }

    [Column("Per_Controlador")]
    public string Controlador { get; set; } = null!;

    [Column("Per_Pagina")]
    public string Pagina { get; set; } = null!;

    [Column("Rol_Id")]
    public int RolId { get; set; }

    public UsuarioEntity Usuario { get; set; } = null!;
}
