namespace EmployeeService.Api.Contracts.Responses;

public record GetEmployeesFromDepartmentResponse
{
    public List<EmployeeDto> Employees { get; set; }
}