using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiante.Adapters.MySql.Entities;

[Table("materiaprofesor")]
public class MateriaProfesorEntity
{
    [Key]
    [Column("MatPro_Id")]
    public int Id { get; set; }

    [Column("Mat_Id")]
    public int IdMateria { get; set; }

    [Column("Usu_Id")]
    public int IdProfesor { get; set; }
}
