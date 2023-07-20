using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Contracts.Requests;

public record GetEmployeesFromCompanyRequest
{
    [Required]
    public int? CompanyId { get; set; }
}