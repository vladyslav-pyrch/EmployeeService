using System.Data;
using Dapper;
using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.GetAllEmployeesFromDepartment;

public class GetAllEmployeesFromDepartmentQueryHandler : IQueryHandler<GetAllEmployeesFromDepartmentQuery, List<Employee>>
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
from departments d
    left join employees e on d.id = e.department_id
    left join passports p on p.id = e.passport_id
    left join passport_types pt on pt.id = p.passport_type_id
where d.id = @DepartmentId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	public GetAllEmployeesFromDepartmentQueryHandler(IsThereDepartmentQueryHandler isThereDepartmentQueryHandler,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public List<Employee> Handle(GetAllEmployeesFromDepartmentQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.Query<EmployeeDto>(Sql, new { DepartmentId = query.DepartmentId.Deconvert() })
			.Select(dto => (Employee)dto)
			.ToList();
	}

	private void CheckQuery(GetAllEmployeesFromDepartmentQuery query)
	{
		var isThereDepartmentQuery = new IsThereDepartmentQuery(
			query.DepartmentId
		);

		if (!_isThereDepartmentQueryHandler.Handle(isThereDepartmentQuery))
			throw new InvalidOperationException("There is no such department");
	}
}