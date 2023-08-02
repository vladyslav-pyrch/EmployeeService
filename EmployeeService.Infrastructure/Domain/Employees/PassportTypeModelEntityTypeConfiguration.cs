using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Domain.Employees;

internal class PassportTypeModelEntityTypeConfiguration : IEntityTypeConfiguration<PassportTypeModel>
{
	public void Configure(EntityTypeBuilder<PassportTypeModel> builder)
	{
		builder.HasKey(type => type.Id);
		builder.HasIndex(type => type.Name)
			.IsUnique();
		builder.Property(type => type.Id)
			.ValueGeneratedNever();
		builder.Property(type => type.Name)
			.HasMaxLength(50)
			.IsRequired();
		builder.HasMany<PassportModel>()
			.WithOne()
			.HasForeignKey(passport => passport.PassportTypeId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}