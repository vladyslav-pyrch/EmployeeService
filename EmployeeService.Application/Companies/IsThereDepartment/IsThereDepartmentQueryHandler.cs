using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;

namespace EmployeeService.Application.Companies.IsThereDepartment;

public class IsThereDepartmentQueryHandler : IQueryHandler<IsThereDepartmentQuery, bool>
{
	private const string Sql = @"
select 
    case 
        when @Id in (select id from departments)
            then 1
        else 0
    end IsThere;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public IsThereDepartmentQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public bool Handle(IsThereDepartmentQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<bool>(Sql, new { Id = query.DepartmentId.Deconvert() });
	}
}