using EmployeeService.Api.Contracts;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromDepartment;

public record GetAllEmployeesFromDepartmentResponse(List<EmployeeDto> Employees);