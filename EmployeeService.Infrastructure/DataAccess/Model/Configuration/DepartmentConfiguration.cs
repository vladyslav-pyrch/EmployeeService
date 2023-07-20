using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.DataAccess.Model.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(department => department.Id);
        builder.Property(department => department.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(department => department.Phone)
            .HasMaxLength(15)
            .IsRequired();
        builder.HasMany<Employee>()
            .WithOne()
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(department => department.CompanyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}