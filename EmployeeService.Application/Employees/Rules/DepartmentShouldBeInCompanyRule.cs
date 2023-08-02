using EmployeeService.Application.Companies.IsThereDepartmentInCompany;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.Rules;

public class DepartmentShouldBeInCompanyRule : BusinessRule<Employee>
{
	private readonly IsThereDepartmentInCompanyQueryHandler _isThereDepartmentInCompanyQueryHandler;

	public DepartmentShouldBeInCompanyRule(IsThereDepartmentInCompanyQueryHandler isThereDepartmentInCompanyQueryHandler)
	{
		_isThereDepartmentInCompanyQueryHandler = isThereDepartmentInCompanyQueryHandler;
	}

	public override string Message => "The workplace is valid only if the department is in the company.";
	
	public override void Check(Employee entity)
	{
		var query = new IsThereDepartmentInCompanyQuery(
			entity.Workplace.Company,
			entity.Workplace.Department
		);

		if (!_isThereDepartmentInCompanyQueryHandler.Handle(query))
			Throw();
	}
}