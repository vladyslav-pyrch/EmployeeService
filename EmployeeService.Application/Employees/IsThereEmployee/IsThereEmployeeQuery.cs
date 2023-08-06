using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.IsThereEmployee;

public class IsThereEmployeeQuery : IQuery<bool>
{
	public IsThereEmployeeQuery(EmployeeId employeeId) => EmployeeId = employeeId;

	public EmployeeId EmployeeId { get; }
}