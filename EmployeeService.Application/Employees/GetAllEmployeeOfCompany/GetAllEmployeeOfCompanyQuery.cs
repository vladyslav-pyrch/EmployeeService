using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetAllEmployeeOfCompany;

public class GetAllEmployeeOfCompanyQuery : IQuery<List<Employee>>
{
    public GetAllEmployeeOfCompanyQuery(CompanyId companyId)
    {
        CompanyId = companyId;
    }
    
    public CompanyId CompanyId { get; }
}