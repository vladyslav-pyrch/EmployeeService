dotnet ef migrations add Initial -o DataAccess/Migrations -p EmployeeService.Infrastructure -s EmployeeService.Api
dotnet ef database update -p EmployeeService.Infrastructure -s EmployeeService.Api