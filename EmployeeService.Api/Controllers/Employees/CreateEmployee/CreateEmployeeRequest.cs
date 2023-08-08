using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.CreateEmployee;

public record CreateEmployeeRequest
{
	[Required]
	[NotNull]
	[StringLength(50)]
	public string? Name { get; set; }

	[Required]
	[NotNull]
	[StringLength(50)]
	public string? Surname { get; set; }

	[Required]
	[NotNull]
	public string? PassportNumber { get; set; }

	[Required]
	[NotNull]
	[StringLength(50)]
	public string? PassportType { get; set; }

	[Required]
	[NotNull]
	[Phone]
	[StringLength(15)]
	public string? PhoneNumber { get; set; }

	[Required]
	[NotNull]
	public int? CompanyId { get; set; }

	[Required]
	[NotNull]
	public int? DepartmentId { get; set; }
}