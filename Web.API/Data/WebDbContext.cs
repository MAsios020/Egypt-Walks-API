using Microsoft.EntityFrameworkCore;
using Web.API.Models.Domain;

namespace Web.API.Data
{
    public class WebDbContext : DbContext
    {
        public WebDbContext( DbContextOptions<WebDbContext> dbContextOptions ) : base(dbContextOptions) 
        {


        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty>  Difficulties { get; set; }
        public DbSet<Region>  Regions { get; set; }

    }
}
