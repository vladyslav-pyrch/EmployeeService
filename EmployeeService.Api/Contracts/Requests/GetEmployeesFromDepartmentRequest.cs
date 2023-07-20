using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Contracts.Requests;

public record GetEmployeesFromDepartmentRequest
{
    [Required]
    public int? DepartmentId { get; set; }
}