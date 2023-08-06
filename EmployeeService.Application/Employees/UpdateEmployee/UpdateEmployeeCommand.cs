using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommand : Command
{
	public UpdateEmployeeCommand(Guid id, EmployeeId employeeId) : base(id)
	{
		EmployeeId = employeeId;
	}
	
	public EmployeeId EmployeeId { get; set; }
	
	public string? Name { get; set; }

	public string? Surname { get; set; }
	
	public Passport? Passport { get; set; }
	
	public PhoneNumber? PhoneNumber { get; set; }
	
	public Workplace? Workplace { get; set; }
}