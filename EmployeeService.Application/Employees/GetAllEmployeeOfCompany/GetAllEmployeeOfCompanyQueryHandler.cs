using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.GetAllEmployeeOfCompany;

public class GetAllEmployeeOfCompanyQueryHandler : IQueryHandler<GetAllEmployeeOfCompanyQuery, List<Employee>>
{
    private const string Sql = @"select
    e.id as Id,
    e.name as Name,
    e.surname as Surname,
    e.phone as PhoneNumber,
    p.number as PassportNumber,
    pt.name as PassportType,
    d.id as DepartmentId,
    d.company_id as CompanyId
from departments d
    left join employees e on d.id = e.department_id
    left join passports p on p.id = e.passport_id
    left join passport_types pt on pt.id = p.passport_type_id
where d.company_id = @CompanyId;";
    
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllEmployeeOfCompanyQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public List<Employee> Handle(GetAllEmployeeOfCompanyQuery query)
    {
        var connection = _sqlConnectionFactory.OpenConnection;
        
        return connection.Query<EmployeeDto>(Sql, new { CompanyId = query.CompanyId.Deconvert() })
            .Select(ConvertToEmployee)
            .ToList();
    }

    private static Employee ConvertToEmployee(EmployeeDto dto)
    {
        var id = new EmployeeId(dto.Id);
        var passport = new Passport(
            new PassportNumber(dto.PassportNumber),
            new PassportType(dto.PassportType)
        );
        var phoneNumber = new PhoneNumber(dto.PhoneNumber);
        var workplace = new Workplace(
            new CompanyId(dto.CompanyId),
            new DepartmentId(dto.DepartmentId)
        );
            
        return new Employee(id, dto.Name, dto.Surname, passport, phoneNumber, workplace);
    }
}