using System.Data;
using Dapper;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Companies.GetAllDepartmentsFromCompany;

public class GetAllDepartmentsFromCompanyQueryHandler :
	IQueryHandler<GetAllDepartmentsFromCompanyQuery, List<Department>>
{
	private const string GetAllDepartmentsFromCompanySql = @"
select 
    d.id as Id,
    d.name as Name,
    d.phone as PhoneNumber,
    d.company_id as CompanyId
from departments d
where d.company_id = @CompanyId";

	private const string GetEmployeeIdsOfEmployeesFromDepartmentSql = @"
select 
    e.id
from employees e
where e.department_id = @DepartmentId";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	public GetAllDepartmentsFromCompanyQueryHandler(IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_sqlConnectionFactory = sqlConnectionFactory;
	}
	
	public List<Department> Handle(GetAllDepartmentsFromCompanyQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.Query<DepartmentDto>(GetAllDepartmentsFromCompanySql, new
			{
				CompanyId = query.CompanyId.Deconvert()
			})
			.Select(dto => Convert.ToDepartment(dto, GetEmployeeIdsOfEmployeesFromDepartment(dto.Id)))
			.ToList();
	}

	private void CheckQuery(GetAllDepartmentsFromCompanyQuery query)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(query.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company");
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