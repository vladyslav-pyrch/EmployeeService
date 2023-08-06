using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.GetNewDepartmentId;

public class GetNewDepartmentIdQueryHandler : IQueryHandler<GetNewDepartmentIdQuery, DepartmentId>
{
	private const string Sql = @"
select (case when min(minid) > 1 then 1 else coalesce(min(t.id) + 1, 0) end)
from departments t left outer join
    departments t2 
        on t.id = t2.id - 1 cross join 
    (select min(id) as minid from departments t) const
where t2.id is null;
";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public GetNewDepartmentIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) =>
		_sqlConnectionFactory = sqlConnectionFactory;

	public DepartmentId Handle(GetNewDepartmentIdQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var id = connection.QuerySingle<int>(Sql);

		return new DepartmentId(id);
	}
}