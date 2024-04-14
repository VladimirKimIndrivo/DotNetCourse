using CompanyManagement.Service.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManagement.Service.Infrastructure.Data.DataModelConfigurations
{
    internal class CompanyDataModelConfiguration : IEntityTypeConfiguration<CompanyDataModel>
    {
        public void Configure(EntityTypeBuilder<CompanyDataModel> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
