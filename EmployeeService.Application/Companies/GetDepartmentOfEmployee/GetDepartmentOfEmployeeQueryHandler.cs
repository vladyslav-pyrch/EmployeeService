using System.Data;
using Dapper;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies.GetDepartmentOfEmployee;

public class GetDepartmentOfEmployeeQueryHandler : IQueryHandler<GetDepartmentOfEmployeeQuery, Department>
{
	private const string Sql = @"select
    d.id as Id,
    d.name as Name,
    d.phone as PhoneNumber,
    d.company_id as CompanyId
from employees e 
    left join departments d on d.id = e.department_id
where e.id = @EmployeeId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	public GetDepartmentOfEmployeeQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
		IsThereEmployeeQueryHandler isThereEmployeeQueryHandler)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
	}

	public Department Handle(GetDepartmentOfEmployeeQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var departmentDto = connection.QuerySingle<DepartmentDto>(Sql, new
		{
			EmployeeId = query.EmployeeId.Deconvert()
		});

		return Convert.ToDepartment(departmentDto);
	}

	private void CheckQuery(GetDepartmentOfEmployeeQuery query)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(query.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee");
	}
}