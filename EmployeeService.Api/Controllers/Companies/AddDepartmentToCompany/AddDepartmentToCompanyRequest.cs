using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.AddDepartmentToCompany;

public record AddDepartmentToCompanyRequest
{
	[Required]
	[NotNull]
	public int? CompanyId { get; set; }

	[Required]
	[NotNull]
	[StringLength(50)]
	public string? DepartmentName { get; set; }

	[Required]
	[NotNull]
	[Phone]
	[StringLength(15)]
	public string? DepartmentPhoneNumber { get; set; }
}