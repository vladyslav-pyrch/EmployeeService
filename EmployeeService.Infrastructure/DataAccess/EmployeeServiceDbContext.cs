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

    public DbSet<EmployeeModel> Employees { get; set; } = null!;

    public DbSet<CompanyModel> Companies { get; set; } = null!;

    public DbSet<DepartmentModel> Departments { get; set; } = null!;

    public DbSet<PassportModel> Passports { get; set; } = null!;

    public DbSet<PassportTypeModel> PassportTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}