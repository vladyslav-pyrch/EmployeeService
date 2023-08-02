using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.Rules;

public class DepartmentShouldExistRule : BusinessRule<Employee>
{
	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	public DepartmentShouldExistRule(IsThereDepartmentQueryHandler isThereDepartmentQueryHandler)
	{
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
	}

	public override string Message => "There is no department with such Id";
	
	public override void Check(Employee entity)
	{
		var query = new IsThereDepartmentQuery(entity.Workplace.Department);
		
		if (!_isThereDepartmentQueryHandler.Handle(query))
			Throw();
	}
}