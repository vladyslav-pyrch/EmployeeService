using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.DeleteCompany;

public record DeleteCompanyRequest
{
	[Required, NotNull] public int? CompanyId { get; set; }
}