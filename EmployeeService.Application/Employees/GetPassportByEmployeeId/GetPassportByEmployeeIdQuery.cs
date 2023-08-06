using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetPassportByEmployeeId;

public class GetPassportByEmployeeIdQuery : IQuery<Passport>
{
	public GetPassportByEmployeeIdQuery(EmployeeId employeeId)
	{
		EmployeeId = employeeId;
	}

	public EmployeeId EmployeeId { get; set; }
}