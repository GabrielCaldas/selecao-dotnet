using Microsoft.EntityFrameworkCore;
namespace API_de_cursos.Models
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
