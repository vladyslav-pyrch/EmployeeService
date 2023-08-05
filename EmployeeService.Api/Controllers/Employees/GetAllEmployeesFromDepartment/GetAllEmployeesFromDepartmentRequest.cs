using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.GetAllEmployeesFromDepartment;

public record GetAllEmployeesFromDepartmentRequest
{
	[Required, NotNull] public int? DepartmentId { get; set; }
}