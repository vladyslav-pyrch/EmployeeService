using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Domain.Employees;

internal class PassportModelEntityTypeConfiguration : IEntityTypeConfiguration<PassportModel>
{
	public void Configure(EntityTypeBuilder<PassportModel> builder)
	{
		builder.HasKey(passport => passport.Id);
		builder.Property(passport => passport.Id)
			.ValueGeneratedNever();
		builder.Property(passport => passport.Number)
			.HasMaxLength(50)
			.IsRequired();
		builder.HasOne<PassportTypeModel>()
			.WithMany()
			.HasForeignKey(passport => passport.PassportTypeId)
			.IsRequired()
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne<EmployeeModel>()
			.WithOne()
			.HasForeignKey<EmployeeModel>(employee => employee.PassportId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade); // Not sure about it.
	}
}