using System.Reflection;
using EmployeeService.Infrastructure.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.DataAccess;

public class EmployeeServiceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    
    public DbSet<Company> Companies { get; set; } = null!;
    
    public DbSet<Department> Departments { get; set; } = null!;
    
    public DbSet<Passport> Passports { get; set; } = null!;
    
    public DbSet<PassportType> PassportTypes { get; set; } = null!;

    public EmployeeServiceDbContext(DbContextOptions options): base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}