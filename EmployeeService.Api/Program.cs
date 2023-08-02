using EmployeeService.Application.Companies.CreateCompany;
using EmployeeService.Application.Companies.GetDepartmentOfEmployee;
using EmployeeService.Application.Companies.GetNewCompanyId;
using EmployeeService.Application.Companies.GetNewDepartmentId;
using EmployeeService.Application.Employees.GetAllEmployeeOfCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Infrastructure.DataAccess;
using EmployeeService.Infrastructure.Domain.Companies;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetDepartmentOfEmployeeQueryHandler>();
builder.Services.AddScoped<GetAllEmployeeOfCompanyQueryHandler>();
builder.Services.AddScoped<GetNewCompanyIdQueryHandler>();
builder.Services.AddScoped<GetNewDepartmentIdQueryHandler>();

builder.Services.AddScoped<CreateCompanyCommandHandler>();

builder.Services.AddScoped<ISqlConnectionFactory, SqliteConnectionFactory>(provider =>
{
	string? connectionString = provider.GetRequiredService<IConfiguration>()
		.GetConnectionString("Database");

	return new SqliteConnectionFactory(connectionString);
});
builder.Services.AddDbContext<EmployeeServiceDbContext>(optionsBuilder =>
{
	optionsBuilder.UseSqlite(
		builder.Configuration.GetConnectionString("Database")
	).UseSnakeCaseNamingConvention();
});
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<DomainEventSource>();

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