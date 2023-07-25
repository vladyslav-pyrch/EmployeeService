using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.DataAccess.Model.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(employee => employee.Id);
        builder.Property(employee => employee.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(employee => employee.Surname)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(employee => employee.Phone)
            .HasMaxLength(15)
            .IsRequired();
        builder.HasOne<Passport>()
            .WithOne()
            .HasForeignKey<Employee>(employee => employee.PassportId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(employee => employee.DepartmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}