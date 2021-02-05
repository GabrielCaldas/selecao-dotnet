using System.ComponentModel.DataAnnotations;

namespace API_de_cursos.Models
{
	public class Matricula
	{
		public long Id { get; set; }

		[Required]
		public long Id_curso { get; set; }

		[Required]
		public long Id_aluno { get; set; }
	}
}
