using Microsoft.EntityFrameworkCore;
namespace API_de_cursos.Models
{
    public class AlunoCartaoContext : DbContext
    {
        public AlunoCartaoContext(DbContextOptions<AlunoCartaoContext> options)
            : base(options)
        {
        }

        public DbSet<AlunoCartao> AlunoCatoes { get; set; }
    }
}
