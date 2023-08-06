using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetWorkplaceByEmployeeId;

public class GetWorkplaceByEmployeeIdQuery : IQuery<Workplace>
{
	public GetWorkplaceByEmployeeIdQuery(EmployeeId employeeId)
	{
		EmployeeId = employeeId;
	}

	public EmployeeId EmployeeId { get; set; }
}