using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.GetAllDepartmentsFromCompany;

public class GetAllDepartmentsFromCompanyQuery : IQuery<List<Department>>
{
	public GetAllDepartmentsFromCompanyQuery(CompanyId companyId)
	{
		CompanyId = companyId;
	}

	public CompanyId CompanyId { get; }
}