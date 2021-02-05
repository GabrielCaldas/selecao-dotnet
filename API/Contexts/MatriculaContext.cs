using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
{
    public class MatriculaContext : DbContext
    {
        public MatriculaContext(DbContextOptions<MatriculaContext> options)
            : base(options)
        {
        }

        public DbSet<Matricula> Matriculas { get; set; }
    }
}
