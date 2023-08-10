using System.Data;
using Dapper;
using EmployeeService.Application.Companies.GetAllDepartmentsFromCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.GetAllCompanies;

public class GetAllCompaniesQueryHandler : IQueryHandler<GetAllCompaniesQuery, List<Company>>
{
	private const string Sql = @"
select 
    c.id as Id,
    c.name as Name
from companies c";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	private readonly GetAllDepartmentsFromCompanyQueryHandler _getAllDepartmentsFromCompanyQueryHandler;

	public GetAllCompaniesQueryHandler(ISqlConnectionFactory sqlConnectionFactory, 
		GetAllDepartmentsFromCompanyQueryHandler getAllDepartmentsFromCompanyQueryHandler)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
		_getAllDepartmentsFromCompanyQueryHandler = getAllDepartmentsFromCompanyQueryHandler;
	}

	public List<Company> Handle(GetAllCompaniesQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.Query<CompanyDto>(Sql).Select(dto =>
		{
			List<Department> departments = _getAllDepartmentsFromCompanyQueryHandler.Handle(
				new GetAllDepartmentsFromCompanyQuery(new CompanyId(dto.Id))
			);

			return Convert.ToCompany(dto, departments);
		}).ToList();
	}
}