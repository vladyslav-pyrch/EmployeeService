using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.UpdateEmployee;

public record UpdateEmployeeRequest
{
	[Required, NotNull] public int? Id { get; set; }
	
	public string? Name { get; set; }

	public string? Surname { get; set; }

	public string? PassportNumber { get; set; }

	public string? PassportType { get; set; }

	public string? PhoneNumber { get; set; }

	public int? CompanyId { get; set; }

	public int? DepartmentId { get; set; }
	
	
}