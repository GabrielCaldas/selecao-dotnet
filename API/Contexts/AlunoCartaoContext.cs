using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
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
