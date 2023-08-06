using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.IsThereCompany;

public class IsThereCompanyQuery : IQuery<bool>
{
	public IsThereCompanyQuery(CompanyId companyId) => CompanyId = companyId;

	public CompanyId CompanyId { get; }
}