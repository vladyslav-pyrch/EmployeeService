using EmployeeService.Infrastructure.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Domain.Companies;

public class DepartmentModelEntityTypeConfiguration : IEntityTypeConfiguration<DepartmentModel>
{
    public void Configure(EntityTypeBuilder<DepartmentModel> builder)
    {
        builder.HasKey(department => department.Id);
        builder.Property(department => department.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(department => department.Phone)
            .HasMaxLength(15)
            .IsRequired();
        builder.HasMany<EmployeeModel>()
            .WithOne()
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<CompanyModel>()
            .WithMany()
            .HasForeignKey(department => department.CompanyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}