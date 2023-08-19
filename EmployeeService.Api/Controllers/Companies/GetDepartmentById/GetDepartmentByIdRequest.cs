using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Companies.GetDepartmentById;

public record GetDepartmentByIdRequest
{
	[Required]
	[NotNull]
	public int? DepartmentId { get; set; }
}