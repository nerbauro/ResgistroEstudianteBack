namespace RegistroEstudiante.Domain.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } 
        public string Nombre { get; set; } 
        public string Clave { get; set; } 
        public string Email { get; set; } 
        public int RolId { get; set; }
        public bool Activo { get; set; }

        //Atributo funcional
        public string NombreRol { get; set; }
    }
}
