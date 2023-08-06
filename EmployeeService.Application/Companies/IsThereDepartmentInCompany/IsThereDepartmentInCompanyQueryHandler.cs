using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;

namespace EmployeeService.Application.Companies.IsThereDepartmentInCompany;

public class IsThereDepartmentInCompanyQueryHandler : IQueryHandler<IsThereDepartmentInCompanyQuery, bool>
{
	private const string Sql = @"
select 
    case d.company_id
        when @CompanyId then 1
        else 0
    end IsThere
from departments d
where d.id = @DepartmentId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public IsThereDepartmentInCompanyQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public bool Handle(IsThereDepartmentInCompanyQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<bool>(Sql, new
		{
			CompanyId = query.CompanyId.Deconvert(),
			DepartmentId = query.DepartmentId.Deconvert()
		});
	}
}