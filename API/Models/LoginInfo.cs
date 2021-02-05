using System.ComponentModel.DataAnnotations;


namespace API_de_cursos.Models
{
	public class LoginInfo
    {

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
