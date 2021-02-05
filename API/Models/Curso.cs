using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;


namespace API_de_cursos.Models
{
	public class Curso
	{
		public long Id { get; set; }

		[Required]
		public string NMCurso { get; set; }

		
		public string DETCurso { get; set; }

	}
}
