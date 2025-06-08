using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReaderRole = "c7bcf862-1d98-4972-a87b-f7a43ae20996";
            var WriterRole = "1877a783-2c41-4531-ba3e-6221f6615b65";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderRole,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = WriterRole,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };

            builder.Entity<IdentityRole>()
                .HasData(roles);

        }
    }
}
