using CompanyManagement.Service.Domain;
using CompanyManagement.Service.Domain.DataModels;
using CompanyManagement.Service.Domain.DataModels.Base;
using CompanyManagement.Service.Infrastructure.Data.DataModelConfigurations.DataModelBuilderExtensions;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace CompanyManagement.Service.Infrastructure.Data
{
    internal class CompanyManagementDbContext : DbContext, ICompanyManagementDbContext
    {
        public CompanyManagementDbContext(DbContextOptions<CompanyManagementDbContext> options) : base(options)
        {   
            
        }

        public DbSet<CompanyDataModel> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CompanyManagement");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyManagementDbContext).Assembly);

            ApplyConfigurationsToDerivedTypes(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                switch (entry.Entity)
                {
                    case BaseDataModel trackable:
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                trackable.Created = DateTime.UtcNow;
                                trackable.Changed = DateTime.UtcNow;
                                break;
                            case EntityState.Modified:
                                trackable.Changed = DateTime.UtcNow;
                                break;
                        }

                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyConfigurationsToDerivedTypes(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (typeof(BaseDataModel).IsAssignableFrom(clrType) && !clrType.IsAbstract)
                {
                    var applyConfigurationMethod = typeof(ModelBuilderExtensions)
                        .GetMethod(nameof(ModelBuilderExtensions.ApplyBaseDataModelEntityConfiguration))
                        ?.MakeGenericMethod(clrType);
                    applyConfigurationMethod?.Invoke(null, new object[] { modelBuilder });
                }
            }
        }
    }
}
