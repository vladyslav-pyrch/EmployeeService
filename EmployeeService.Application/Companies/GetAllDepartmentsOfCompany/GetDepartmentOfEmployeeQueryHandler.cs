using Dapper;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies.GetAllDepartmentsOfCompany;

public class GetDepartmentOfEmployeeQueryHandler : IQueryHandler<GetDepartmentOfEmployeeQuery, Department>
{
    private const string Sql = @"select
    d.id as Id,
    d.name as Name,
    d.phone as PhoneNumber,
    d.company_id as CompanyId
from employees e 
    left join departments d on d.id = e.department_id
where e.id = @EmployeeId;";

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetDepartmentOfEmployeeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public Department Handle(GetDepartmentOfEmployeeQuery query)
    {
        var connection = _sqlConnectionFactory.OpenConnection;

        var departmentDto = connection.QuerySingle<DepartmentDto>(Sql, new { EmployeeId = query.EmployeeId.Deconvert() });

        return ConvertToDepartment(departmentDto);
    }

    private Department ConvertToDepartment(DepartmentDto departmentDto)
    {
        var id = new DepartmentId(departmentDto.Id);
        var name = departmentDto.Name;
        var phoneNumber = new PhoneNumber(departmentDto.PhoneNumber);
        var companyId = new CompanyId(departmentDto.CompanyId);

        return new Department(id, name, phoneNumber, companyId);
    }
}