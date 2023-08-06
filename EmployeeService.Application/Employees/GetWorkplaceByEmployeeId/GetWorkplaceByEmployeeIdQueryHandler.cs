using System.Data;
using Dapper;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetWorkplaceByEmployeeId;

public class GetWorkplaceByEmployeeIdQueryHandler : IQueryHandler<GetWorkplaceByEmployeeIdQuery, Workplace>
{
	private const string Sql = @"
select 
    d.company_id as CompanyId,
    d.id as DepartmentId
from employees e
    left join departments d on e.department_id = d.id
where e.id = @EmployeeId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	public GetWorkplaceByEmployeeIdQueryHandler(IsThereEmployeeQueryHandler isThereEmployeeQueryHandler,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public Workplace Handle(GetWorkplaceByEmployeeIdQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var workplaceDto = connection.QuerySingle<WorkplaceDto>(Sql, new
		{
			EmployeeId = query.EmployeeId.Deconvert()
		});

		return Convert.ToWorkplace(workplaceDto);
	}
	
	private void CheckQuery(GetWorkplaceByEmployeeIdQuery query)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(query.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee.");
	}
}