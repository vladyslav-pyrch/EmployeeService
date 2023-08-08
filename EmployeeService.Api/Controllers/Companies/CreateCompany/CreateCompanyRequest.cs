using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.CreateCompany;

public record CreateCompanyRequest
{
	[Required]
	[NotNull]
	[StringLength(50)]
	public string? Name { get; set; }

	[Required]
	[NotNull]
	[Phone]
	[StringLength(15)]
	public string? PhoneNumber { get; set; }
}