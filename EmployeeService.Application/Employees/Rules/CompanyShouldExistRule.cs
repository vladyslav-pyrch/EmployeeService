using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.Rules;

public class CompanyShouldExistRule : BusinessRule<Employee>
{
	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	public CompanyShouldExistRule(IsThereCompanyQueryHandler isThereCompanyQueryHandler)
	{
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
	}

	public override string Message => "There is no company with such Id";
	
	public override void Check(Employee entity)
	{
		var query = new IsThereCompanyQuery(entity.Workplace.Company);
		
		if (!_isThereCompanyQueryHandler.Handle(query))
			Throw();
	}
}