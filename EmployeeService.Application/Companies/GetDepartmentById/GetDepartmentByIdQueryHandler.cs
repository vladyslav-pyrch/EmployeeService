using System.Data;
using Dapper;
using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Companies.GetDepartmentById;

public class GetDepartmentByIdQueryHandler : IQueryHandler<GetDepartmentByIdQuery, Department>
{
	private const string GetDepartmentSql = @"
select
	d.id as Id,
	d.name as Name,
	d.phone as PhoneNumber,
	d.company_id as CompanyId
from departments d
where d.id = @DepartmentId";
    
	private const string GetEmployeeIdsOfEmployeesFromDepartmentSql = @"
select 
    e.id
from employees e
where e.department_id = @DepartmentId";
	
	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public GetDepartmentByIdQueryHandler(IsThereDepartmentQueryHandler isThereDepartmentQueryHandler,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public Department Handle(GetDepartmentByIdQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var departmentDto = connection.QuerySingle<DepartmentDto>(GetDepartmentSql, new
		{
			DepartmentId = query.DepartmentId.Deconvert()
		});

		return Convert.ToDepartment(departmentDto, GetEmployeeIdsOfEmployeesFromDepartment(departmentDto.Id));
	}

	private void CheckQuery(GetDepartmentByIdQuery query)
	{
		var isThereDepartmentQuery = new IsThereDepartmentQuery(
			query.DepartmentId
		);

		if (!_isThereDepartmentQueryHandler.Handle(isThereDepartmentQuery))
			throw new InvalidOperationException("There is no such department");
	}

	private List<EmployeeId> GetEmployeeIdsOfEmployeesFromDepartment(int departmentId)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.Query<int>(GetEmployeeIdsOfEmployeesFromDepartmentSql, new
			{
				DepartmentId = departmentId
			})
			.Select(employeeId => new EmployeeId(employeeId))
			.ToList();
	}
}