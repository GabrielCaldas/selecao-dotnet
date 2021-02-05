using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos.Contexts
{
    public class SessionTokenContext : DbContext
{
    public SessionTokenContext(DbContextOptions<SessionTokenContext> options)
        : base(options)
    {
    }

    public DbSet<SessionToken> Tokens { get; set; }
}
}
