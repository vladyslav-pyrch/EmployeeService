using System.Data;
using Dapper;
using EmployeeService.Application.Companies.GetAllDepartmentsFromCompany;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.GetCompanyById;

public class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, Company>
{
	private const string Sql = @"
select 
    id as Id,
    name as Name
from companies c
where c.id = @ComapanyId";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly GetAllDepartmentsFromCompanyQueryHandler _getAllDepartmentsFromCompanyQueryHandler;

	public GetCompanyByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
		IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		GetAllDepartmentsFromCompanyQueryHandler getAllDepartmentsFromCompanyQueryHandler)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_getAllDepartmentsFromCompanyQueryHandler = getAllDepartmentsFromCompanyQueryHandler;
	}

	public Company Handle(GetCompanyByIdQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var companyDto = connection.QuerySingle<CompanyDto>(Sql, new
		{
			ComapanyId = query.CompanyId.Deconvert()
		});

		List<Department> departments = _getAllDepartmentsFromCompanyQueryHandler.Handle(
			new GetAllDepartmentsFromCompanyQuery(query.CompanyId)
		);

		return Convert.ToCompany(companyDto, departments);
	}
	

	private void CheckQuery(GetCompanyByIdQuery query)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(query.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company");
	}
}