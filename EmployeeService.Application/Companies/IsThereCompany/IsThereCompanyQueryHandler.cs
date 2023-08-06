using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;

namespace EmployeeService.Application.Companies.IsThereCompany;

public class IsThereCompanyQueryHandler : IQueryHandler<IsThereCompanyQuery, bool>
{
	private const string Sql = @"
select 
    case 
        when @Id in (select id from companies)
            then 1
        else 0
    end IsThere;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public IsThereCompanyQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public bool Handle(IsThereCompanyQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<bool>(Sql, new { Id = query.CompanyId.Deconvert() });
	}
}