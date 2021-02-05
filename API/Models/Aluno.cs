using System.ComponentModel.DataAnnotations;

namespace API_de_cursos.Models
{
	public class Aluno
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
