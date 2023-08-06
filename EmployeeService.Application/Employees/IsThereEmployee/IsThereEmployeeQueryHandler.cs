using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;

namespace EmployeeService.Application.Employees.IsThereEmployee;

public class IsThereEmployeeQueryHandler : IQueryHandler<IsThereEmployeeQuery, bool>
{
	private const string Sql = @"
select 
    case 
        when @Id in (select id from employees)
            then 1
        else 0
    end IsThere;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public IsThereEmployeeQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public bool Handle(IsThereEmployeeQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<bool>(Sql, new { Id = query.EmployeeId.Deconvert() });
	}
}