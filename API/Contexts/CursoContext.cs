using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
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
