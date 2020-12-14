using Microsoft.EntityFrameworkCore;
namespace API_de_cursos.Models
{
    public class AlunoPGTContext : DbContext
    {
        public AlunoPGTContext(DbContextOptions<AlunoPGTContext> options)
            : base(options)
        {
        }

        public DbSet<AlunoPGT> AlunoPagamentos { get; set; }
    }
}
