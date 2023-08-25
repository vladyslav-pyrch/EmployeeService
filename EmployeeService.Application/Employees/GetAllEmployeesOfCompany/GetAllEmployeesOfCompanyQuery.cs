using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.GetAllEmployeesOfCompany;

public class GetAllEmployeesOfCompanyQuery : IQuery<List<Employee>>
{
	public GetAllEmployeesOfCompanyQuery(CompanyId companyId) => CompanyId = companyId;

	public CompanyId CompanyId { get; }
}