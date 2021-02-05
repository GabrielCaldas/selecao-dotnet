using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
{
	public class AlunoContext: DbContext
    {
        public AlunoContext(DbContextOptions<AlunoContext> options)
            : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
}
}
