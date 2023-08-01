using EmployeeService.Application.Companies.GetAllDepartmentsOfCompany;
using EmployeeService.Application.Employees.GetAllEmployeeOfCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<GetDepartmentOfEmployeeQueryHandler>();
builder.Services.AddScoped<GetAllEmployeeOfCompanyQueryHandler>();
builder.Services.AddScoped<ISqlConnectionFactory, SqliteConnectionFactory>(provider =>
{
	string? connectionString = provider.GetRequiredService<IConfiguration>()
		.GetConnectionString("Database");

	return new SqliteConnectionFactory(connectionString);
});
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<DomainEventSource>();
builder.Services.AddDbContext<EmployeeServiceDbContext>(optionsBuilder =>
{
	optionsBuilder.UseSqlite(
		builder.Configuration.GetConnectionString("Database")
	).UseSnakeCaseNamingConvention();
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();