using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.UpdateEmployee;

public record UpdateEmployeeRequest
{
	[Required]
	[NotNull]
	public int? Id { get; set; }

	[StringLength(50)]
	public string? Name { get; set; }

	[StringLength(50)]
	public string? Surname { get; set; }

	public string? PassportNumber { get; set; }

	[StringLength(50)]
	public string? PassportType { get; set; }

	[StringLength(15)]
	public string? PhoneNumber { get; set; }

	public int? CompanyId { get; set; }

	public int? DepartmentId { get; set; }
}