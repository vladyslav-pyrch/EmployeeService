using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdQuery : IQuery<Employee>
{
	public GetEmployeeByIdQuery(EmployeeId employeeId)
	{
		EmployeeId = employeeId;
	}

	public EmployeeId EmployeeId { get; set; }
}