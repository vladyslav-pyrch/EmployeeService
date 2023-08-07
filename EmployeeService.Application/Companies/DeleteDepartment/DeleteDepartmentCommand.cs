using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.DeleteDepartment;

public class DeleteDepartmentCommand : Command
{
	public DeleteDepartmentCommand(Guid id, DepartmentId departmentId) : base(id)
	{
		DepartmentId = departmentId;
	}
	
	public DepartmentId DepartmentId { get; }
}