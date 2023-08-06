using System.Data;
using Dapper;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetPassportByEmployeeId;

public class GetPassportByEmployeeIdQueryHandler : IQueryHandler<GetPassportByEmployeeIdQuery, Passport>
{
	private const string Sql = @"
select 
    p.number as Number,
    pt.name as Type
from employees e
    left join passports p on e.passport_id = p.id
    left join passport_types pt on pt.id = p.passport_type_id
where e.id = @EmployeeId;";

	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	public GetPassportByEmployeeIdQueryHandler(IsThereEmployeeQueryHandler isThereEmployeeQueryHandler,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
		_sqlConnectionFactory = sqlConnectionFactory;
	}
	
	public Passport Handle(GetPassportByEmployeeIdQuery query)
	{
		CheckQuery(query);

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		var passportDto = connection.QuerySingle<PassportDto>(Sql, new
		{
			EmployeeId = query.EmployeeId.Deconvert()
		});

		return Convert.ToPassport(passportDto);
	}
	
	private void CheckQuery(GetPassportByEmployeeIdQuery query)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(query.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee.");
	}
}