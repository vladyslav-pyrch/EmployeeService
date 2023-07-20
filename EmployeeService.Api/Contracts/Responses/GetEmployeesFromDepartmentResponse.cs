using EmployeeService.Api.Contracts.Models;

namespace EmployeeService.Api.Contracts.Responses;

public record GetEmployeesFromDepartmentResponse
{
    public List<Employee> Employees { get; set; }
}