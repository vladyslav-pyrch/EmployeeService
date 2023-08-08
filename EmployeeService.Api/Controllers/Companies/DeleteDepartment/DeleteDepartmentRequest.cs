using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.DeleteDepartment;

public record DeleteDepartmentRequest
{
	[Required]
	[NotNull]
	public int? DepartmentId { get; set; }
}