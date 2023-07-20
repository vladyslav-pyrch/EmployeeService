using EmployeeService.Api.Contracts.Models;

namespace EmployeeService.Api.Contracts.Responses;

public record GetEmployeesFromCompanyResponse
{
    public List<Employee> Employees { get; set; }
}