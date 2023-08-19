using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies.UpdateDepartment;

public class UpdateDepartmentCommand : Command
{
	public UpdateDepartmentCommand(Guid id, DepartmentId departmentId) : base(id)
	{
		DepartmentId = departmentId;
	}
	
	public DepartmentId DepartmentId { get; set; }
	
	public string? Name { get; set; }
	
	public PhoneNumber? PhoneNumber { get; set; }
}