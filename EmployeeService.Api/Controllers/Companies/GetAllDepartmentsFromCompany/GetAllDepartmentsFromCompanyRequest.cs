using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.GetAllDepartmentsFromCompany;

public record GetAllDepartmentsFromCompanyRequest
{
	[Required]
	[NotNull]
	public int? CompanyId { get; set; }
}