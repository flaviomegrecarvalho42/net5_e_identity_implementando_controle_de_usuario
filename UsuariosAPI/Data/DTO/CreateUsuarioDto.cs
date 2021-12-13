using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.DTO
{
    public class CreateUsuarioDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Repassword { get; set; }
    }
}
