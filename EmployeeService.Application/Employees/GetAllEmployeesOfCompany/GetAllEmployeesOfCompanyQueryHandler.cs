using System.Data;
using Dapper;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetAllEmployeesOfCompany;

public class GetAllEmployeesOfCompanyQueryHandler : IQueryHandler<GetAllEmployeesOfCompanyQuery, List<Employee>>
{
	private const string Sql = @"select
		e.id as Id,
		e.name as Name,
		e.surname as Surname,
		e.phone as PhoneNumber,
		p.number as PassportNumber,
		pt.name as PassportType,
		d.id as DepartmentId,
		d.company_id as CompanyId
	from departments d
		left join employees e on d.id = e.department_id
		left join passports p on p.id = e.passport_id
		left join passport_types pt on pt.id = p.passport_type_id
	where d.company_id = @CompanyId and e.id is not null";

	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public GetAllEmployeesOfCompanyQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
		IsThereCompanyQueryHandler isThereCompanyQueryHandler)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
	}

	public List<Employee> Handle(GetAllEmployeesOfCompanyQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.Query<EmployeeDto>(Sql, new { CompanyId = query.CompanyId.Deconvert() })
			.Select(Convert.ToEmployee)
			.ToList();
	}

	private void CheckQuery(GetAllEmployeesOfCompanyQuery query)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(query.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company");
	}
}