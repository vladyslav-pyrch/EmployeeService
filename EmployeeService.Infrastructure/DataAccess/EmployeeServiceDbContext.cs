using System.Reflection;
using EmployeeService.Infrastructure.Domain.Companies;
using EmployeeService.Infrastructure.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.DataAccess;

public class EmployeeServiceDbContext : DbContext
{
	public EmployeeServiceDbContext(DbContextOptions options) : base(options)
	{
	}

	internal DbSet<EmployeeModel> Employees { get; set; } = null!;

	internal DbSet<CompanyModel> Companies { get; set; } = null!;

	internal DbSet<DepartmentModel> Departments { get; set; } = null!;

	internal DbSet<PassportModel> Passports { get; set; } = null!;

	internal DbSet<PassportTypeModel> PassportTypes { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}