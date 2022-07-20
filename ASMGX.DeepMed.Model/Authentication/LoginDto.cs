using System.ComponentModel.DataAnnotations;

namespace ASMGX.DeepMed.Model.Authentication
{
    public class LoginDto
    {
        [Required]
        public string Identity { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
