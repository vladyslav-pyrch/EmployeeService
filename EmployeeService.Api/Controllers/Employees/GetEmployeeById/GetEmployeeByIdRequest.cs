using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.GetEmployeeById;

public record GetEmployeeByIdRequest
{
	[Required]
	[NotNull]
	public int? EmployeeId { get; set; }
}