using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiante.Adapters.MySql.Entities;

[Table("rol")]
public class RolEntity
{
    [Key]
    [Column("Rol_Id")]
    public int Id { get; set; }

    [Column("Rol_Nombre")]
    public string Nombre { get; set; }
}
