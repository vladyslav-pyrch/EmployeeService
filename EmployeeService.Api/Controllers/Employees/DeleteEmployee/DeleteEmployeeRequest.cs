using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeService.Api.Controllers.Employees.DeleteEmployee;

public class DeleteEmployeeRequest
{
	[Required] [NotNull] public int? EmployeeId { get; set; }
}