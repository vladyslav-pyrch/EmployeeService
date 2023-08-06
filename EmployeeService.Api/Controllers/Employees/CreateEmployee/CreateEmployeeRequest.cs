using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.CreateEmployee;

public record CreateEmployeeRequest
{
	[Required] [NotNull] public string? Name { get; set; }

	[Required] [NotNull] public string? Surname { get; set; }

	[Required] [NotNull] public string? PassportNumber { get; set; }

	[Required] [NotNull] public string? PassportType { get; set; }

	[Required] [NotNull] public string? PhoneNumber { get; set; }

	[Required] [NotNull] public int? CompanyId { get; set; }

	[Required] [NotNull] public int? DepartmentId { get; set; }
}