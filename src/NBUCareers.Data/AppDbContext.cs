using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NBUCareers.Models;

namespace NBUCareers.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
