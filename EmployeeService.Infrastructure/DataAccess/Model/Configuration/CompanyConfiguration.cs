using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.DataAccess.Model.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(company => company.Id);
        builder.Property(company => company.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasMany<Department>()
            .WithOne()
            .HasForeignKey(department => department.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}