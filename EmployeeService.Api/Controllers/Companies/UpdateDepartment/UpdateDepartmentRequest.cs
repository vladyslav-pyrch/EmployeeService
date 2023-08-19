using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.UpdateDepartment;

public record UpdateDepartmentRequest
{
	[Required]
	[NotNull]
	public int? DepartmentId { get; set; }
	
	public string? Name { get; set; }
	
	public string? PhoneNumber { get; set; }
}