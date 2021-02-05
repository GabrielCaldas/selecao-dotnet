using System.ComponentModel.DataAnnotations;

namespace API_de_cursos.Models
{
	public class SessionToken
	{
		public long Id { get; set; }
		[Required]
		public long Id_User { get; set; }
		[Required]
		public string Token { get; set; }
	}
}
