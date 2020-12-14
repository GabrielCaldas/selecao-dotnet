using System.ComponentModel.DataAnnotations;

namespace API_de_cursos.Models
{
	public class AlunoCartao
    {
        public long Id { get; set; }

        [Required]
        public long Id_aluno { get; set; }

        [Required]
        public long NUCartao { get; set; }

        [Required]
        public long CDVERIFICACAO { get; set; }
    }
}
