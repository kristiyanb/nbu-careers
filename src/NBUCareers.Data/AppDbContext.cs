namespace NBUCareers.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using NBUCareers.Infrastucture.Services;
    using NBUCareers.Models;
    using NBUCareers.Models.Base;

    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IUserService userService;

        public AppDbContext(DbContextOptions options, IUserService userService)
            : base(options)
        {
            this.userService = userService;
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInformation()
            => this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var userName = this.userService.GetUserName();

                    if (entry.Entity is Entity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = userName;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifiedOn = DateTime.UtcNow;
                            entity.ModifiedBy = userName;
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            entity.DeletedOn = DateTime.UtcNow;
                            entity.DeletedBy = userName;
                            entity.IsDeleted = true;

                            entry.State = EntityState.Modified;
                        }
                    }
                });
    }
}
