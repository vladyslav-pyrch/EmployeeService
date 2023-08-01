using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromCompany;

public record GetAllEmployeesFromCompanyRequest
{
    [Required]
    public int? CompanyId { get; set; }
}