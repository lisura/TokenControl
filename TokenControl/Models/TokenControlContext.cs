using Microsoft.EntityFrameworkCore;

namespace TokenControl.Models
{
    public class TokenControlContext : DbContext
    {
        public TokenControlContext(DbContextOptions<TokenControlContext> options) : base(options)
        {
        }

        public virtual DbSet<CardControl> CardControl { get; set; }
    }
}
