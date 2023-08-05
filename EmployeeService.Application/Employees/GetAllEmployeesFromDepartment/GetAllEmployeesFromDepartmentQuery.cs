using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetAllEmployeesFromDepartment;

public class GetAllEmployeesFromDepartmentQuery : IQuery<List<Employee>>
{
	public GetAllEmployeesFromDepartmentQuery(DepartmentId departmentId)
	{
		DepartmentId = departmentId;
	}

	public DepartmentId DepartmentId { get; }
}