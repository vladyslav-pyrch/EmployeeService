using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.UpdateCompany;

public record UpdateCompanyRequest
{
	[Required]
	[NotNull]
	public int? CompanyId { get; set; }
	
	public string? Name { get; set; }
}