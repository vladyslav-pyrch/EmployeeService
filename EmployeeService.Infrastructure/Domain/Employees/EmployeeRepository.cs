using System.Data;
using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Infrastructure.DataAccess;

namespace EmployeeService.Infrastructure.Domain.Employees;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly EmployeeServiceDbContext _dbContext;

	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public EmployeeRepository(EmployeeServiceDbContext dbContext,
		ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public void AddEmployee(Employee employee)
	{
		if (!IsTherePassportType(employee.Passport.Type.Name))
			CreatePassportType(employee.Passport.Type.Name);

		int passportTypeId = GetPassportTypeId(employee.Passport.Type.Name);
		
		CreatePassport(out int passportId, employee.Passport.Number.Number, passportTypeId);

		var employeeModel = new EmployeeModel(
			employee.Identity.Deconvert(),
			employee.Name,
			employee.Surname,
			employee.PhoneNumber.Number,
			employee.Workplace.Department.Deconvert(),
			passportId
		);

		_dbContext.Employees.Add(employeeModel);
	}

	public void DeleteById(EmployeeId employeeId)
	{
		throw new NotImplementedException();
	}

	public void Save() => _dbContext.SaveChanges();
	
	private void CreatePassportType(string name)
	{
		int id = GetNewPassportTypeId();
		
		var passportTypeModel = new PassportTypeModel(id, name);

		 _dbContext.PassportTypes.Add(passportTypeModel);
	}

	private void CreatePassport(out int id, string number, int passportTypeId)
	{
		id = GetNewPassportId();

		var passportModel = new PassportModel(id, number, passportTypeId);

		_dbContext.Passports.Add(passportModel);
	}

	private bool IsTherePassportType(string name)
	{
		const string sql = @"
select 
    case 
        when @Name in (select name from passport_types)
            then 1
        else 0
    end IsThere";

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;
		
		return connection.QuerySingle<bool>(sql, new { Name = name });
	}

	private int GetNewPassportTypeId()
	{
		const string sql = @"
select (case when min(minid) > 1 then 1 else coalesce(min(t.id) + 1, 0) end)
from passport_types t left outer join
    passport_types t2 
        on t.id = t2.id - 1 cross join 
    (select min(id) as minid from passport_types t) const
where t2.id is null;";
		
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<int>(sql);
	}

	private int GetNewPassportId()
	{
		const string sql = @"
select (case when min(minid) > 1 then 1 else coalesce(min(t.id) + 1, 0) end)
from passports t left outer join
    passports t2 
        on t.id = t2.id - 1 cross join 
    (select min(id) as minid from passports t) const
where t2.id is null;";

		IDbConnection connection = _sqlConnectionFactory.OpenConnection;
		
		return connection.QuerySingle<int>(sql);
	}

	private int GetPassportTypeId(string name)
	{
		const string sql = @"
select 
    pt.id
from passport_types pt
where pt.name = @Name;";
		
		IDbConnection connection = _sqlConnectionFactory.OpenConnection;

		return connection.QuerySingle<int>(sql, new { Name = name });
	}
}