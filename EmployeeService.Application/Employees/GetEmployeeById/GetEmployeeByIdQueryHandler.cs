using System.Data;
using Dapper;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Employee>
{
	private const string Sql = @"
select 
    e.id as Id,
    e.name as Name,
    e.surname as Surname,
    e.phone as PhoneNumber,
    p.number as PassportNumber,
    pt.name as PassportType,
    d.id as DepartmentId,
    d.company_id as CompanyId 
from employees e
	left join passports p on p.id = e.passport_id
	left join passport_types pt on pt.id = p.passport_type_id
	left join departments d on d.id = e.department_id
where e.id = @EmployeeId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	public GetEmployeeByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
		IsThereEmployeeQueryHandler isThereEmployeeQueryHandler)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
	}

	public Employee Handle(GetEmployeeByIdQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var employeeDto = connection.QuerySingle<EmployeeDto>(Sql, new
		{
			EmployeeId = query.EmployeeId.Deconvert()
		});

		return Convert.ToEmployee(employeeDto);
	}
	
	private void CheckQuery(GetEmployeeByIdQuery query)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(query.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee.");
	}
}