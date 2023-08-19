using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.GetDepartmentById;

public class GetDepartmentByIdQuery : IQuery<Department>
{
	public GetDepartmentByIdQuery(DepartmentId departmentId)
	{
		DepartmentId = departmentId;
	}
    
	public DepartmentId DepartmentId { get; set; }
}