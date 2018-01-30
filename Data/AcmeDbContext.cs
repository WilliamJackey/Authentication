using AcmeWidget.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.Data
{
    public class AcmeDbContext : IdentityDbContext<AuthenticationUser>
    {
      public AcmeDbContext(DbContextOptions options)
              : base(options)
      {
      }

      public DbSet<AcmeUser> AcmeUsers { get; set; }
    }
}
