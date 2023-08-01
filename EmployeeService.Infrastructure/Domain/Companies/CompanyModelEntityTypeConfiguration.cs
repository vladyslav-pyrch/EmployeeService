using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Domain.Companies;

public class CompanyModelEntityTypeConfiguration : IEntityTypeConfiguration<CompanyModel>
{
	public void Configure(EntityTypeBuilder<CompanyModel> builder)
	{
		builder.HasKey(company => company.Id);
		builder.Property(company => company.Name)
			.HasMaxLength(50)
			.IsRequired();
		builder.HasMany<DepartmentModel>()
			.WithOne()
			.HasForeignKey(department => department.CompanyId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}