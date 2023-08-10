using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.GetCompanyById;

public record GetCompanyByIdRequest
{
	[Required]
	[NotNull]
	public int? CompanyId { get; set; }
}