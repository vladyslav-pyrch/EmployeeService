using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Contracts.Requests;

public record DeleteEmployeeRequest
{
    [Required]
    public int? EmployeeId { get; set; }
}