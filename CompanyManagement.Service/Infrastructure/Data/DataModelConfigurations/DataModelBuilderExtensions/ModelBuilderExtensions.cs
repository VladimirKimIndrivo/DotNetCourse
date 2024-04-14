using CompanyManagement.Service.Domain.DataModels.Base;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Service.Infrastructure.Data.DataModelConfigurations.DataModelBuilderExtensions
{
    internal static class ModelBuilderExtensions
    {
        public static void ApplyBaseDataModelEntityConfiguration<T>(this ModelBuilder modelBuilder)
            where T : BaseDataModel
        {
            // Configure each entity of type BaseDataModel or derived from it
            modelBuilder.Entity<T>(builder =>
            {
                // Primary key configuration
                builder.HasKey(e => e.Id);

                // Property configurations
                builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(e => e.Created).IsRequired();
                builder.Property(e => e.Changed).IsRequired();
                builder.Property(e => e.IsDeleted).IsRequired();
                builder.Property(e => e.Version).IsRequired().IsConcurrencyToken();
            });
        }
    }
}
