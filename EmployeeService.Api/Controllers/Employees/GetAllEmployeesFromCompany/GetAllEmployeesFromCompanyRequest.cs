using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromCompany;

public record GetAllEmployeesFromCompanyRequest
{
	[Required] [NotNull] public int? CompanyId { get; set; }
}