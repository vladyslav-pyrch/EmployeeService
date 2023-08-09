using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.GetCompanyById;

public class GetCompanyByIdQuery : IQuery<Company>
{
	public GetCompanyByIdQuery(CompanyId companyId)
	{
		CompanyId = companyId;
	}

	public CompanyId CompanyId { get; }
}