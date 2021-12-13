using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActivationCode { get; set; }
    }
}
