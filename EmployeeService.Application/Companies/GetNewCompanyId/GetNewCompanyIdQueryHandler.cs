using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.GetNewCompanyId;

public class GetNewCompanyIdQueryHandler : IQueryHandler<GetNewCompanyIdQuery, CompanyId>
{
	private const string Sql = @"
select (case when min(minid) > 1 then 1 else coalesce(min(t.id) + 1, 0) end)
from companies t left outer join
    companies t2 
        on t.id = t2.id - 1 cross join 
    (select min(id) as minid from companies t) const
where t2.id is null;
";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public GetNewCompanyIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public CompanyId Handle(GetNewCompanyIdQuery query)
	{
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var id = connection.QuerySingle<int>(Sql);

		return new CompanyId(id);
	}
}