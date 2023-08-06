using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.CreateCompany;

public record CreateCompanyRequest
{
	[Required] [NotNull] public string? Name { get; set; }

	[Required] [NotNull] public string? PhoneNumber { get; set; }
}