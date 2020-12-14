using Microsoft.EntityFrameworkCore;

namespace API_de_cursos.Models
{
    public class CursoContext : DbContext
    {
        public CursoContext(DbContextOptions<CursoContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
    }
}
