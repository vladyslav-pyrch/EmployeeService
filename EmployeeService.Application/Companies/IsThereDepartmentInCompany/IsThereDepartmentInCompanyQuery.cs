using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.IsThereDepartmentInCompany;

public class IsThereDepartmentInCompanyQuery : IQuery<bool>
{
	public IsThereDepartmentInCompanyQuery(CompanyId companyId, DepartmentId departmentId)
	{
		CompanyId = companyId;
		DepartmentId = departmentId;
	}
	
	public CompanyId CompanyId { get; }
	
	public DepartmentId DepartmentId { get; }
}