using EmployeeService.Infrastructure.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Domain.Employees;

public class EmployeeModelEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeModel>
{
	public void Configure(EntityTypeBuilder<EmployeeModel> builder)
	{
		builder.HasKey(employee => employee.Id);
		builder.Property(employee => employee.Id)
			.ValueGeneratedNever();
		builder.Property(employee => employee.Name)
			.HasMaxLength(50)
			.IsRequired();
		builder.Property(employee => employee.Surname)
			.HasMaxLength(50)
			.IsRequired();
		builder.Property(employee => employee.Phone)
			.HasMaxLength(15)
			.IsRequired();
		builder.HasOne<PassportModel>()
			.WithOne()
			.HasForeignKey<EmployeeModel>(employee => employee.PassportId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();
		builder.HasOne<DepartmentModel>()
			.WithMany()
			.HasForeignKey(employee => employee.DepartmentId)
			.IsRequired()
			.OnDelete(DeleteBehavior.NoAction);
	}
}