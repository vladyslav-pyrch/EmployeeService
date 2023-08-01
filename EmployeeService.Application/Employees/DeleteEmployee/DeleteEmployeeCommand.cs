using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.DeleteEmployee;

public class DeleteEmployeeCommand : Command
{
	public DeleteEmployeeCommand(Guid id, EmployeeId employeeId) : base(id) => EmployeeId = employeeId;

	public EmployeeId EmployeeId { get; }
}