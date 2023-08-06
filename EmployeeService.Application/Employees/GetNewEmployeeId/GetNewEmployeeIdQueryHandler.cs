using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetNewEmployeeId;

public class GetNewEmployeeIdQueryHandler : IQueryHandler<GetNewEmployeeIdQuery, EmployeeId>
{
	private const string Sql = @"
select (case when min(minid) > 1 then 1 else coalesce(min(t.id) + 1, 0) end)
from employees t left outer join
    employees t2 
        on t.id = t2.id - 1 cross join 
    (select min(id) as minid from employees t) const
where t2.id is null;
";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public GetNewEmployeeIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public EmployeeId Handle(GetNewEmployeeIdQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var id = connection.QuerySingle<int>(Sql);

		return new EmployeeId(id);
	}
}