using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiante.Adapters.MySql.Entities;

[Table("materia")]
public class MateriaEntity
{
    [Key]
    [Column("Mat_Id")]
    public int Id { get; set; }

    [Column("Mat_Codigo")]
    public string Codigo { get; set; } = null!;

    [Column("Mat_Descripcion")]
    public string Descripcion { get; set; } = null!;

    [Column("Mat_Creditos")]
    public int Creditos { get; set; }
}
