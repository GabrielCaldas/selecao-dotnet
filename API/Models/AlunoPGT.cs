using System.ComponentModel.DataAnnotations;



namespace API_de_cursos.Models
{
	public class AlunoPGT
	{
		
		public long Id { get; set;}

		[Required]
		public string DTPagamento { get; set; }

		[Required] 
		public long VLPagamento { get; set; }

		[Required]
		public long Id_aluno { get; set; }

		[Required]
		public long Id_cartao { get; set; }
	}
}
