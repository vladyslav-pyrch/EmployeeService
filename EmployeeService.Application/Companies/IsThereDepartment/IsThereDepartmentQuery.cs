using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.IsThereDepartment;

public class IsThereDepartmentQuery : IQuery<bool>
{
	public IsThereDepartmentQuery(DepartmentId departmentId)
	{
		DepartmentId = departmentId;
	}
	
	public DepartmentId DepartmentId { get; }
}