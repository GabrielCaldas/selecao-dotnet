using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
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
