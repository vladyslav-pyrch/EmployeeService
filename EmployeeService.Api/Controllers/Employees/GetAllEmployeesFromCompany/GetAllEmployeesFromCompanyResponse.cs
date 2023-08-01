using EmployeeService.Api.Contracts;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromCompany;

public record GetAllEmployeesFromCompanyResponse(List<EmployeeDto> Employees);