using Microsoft.EntityFrameworkCore;
namespace API_de_cursos.Models
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
