using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.DeleteCompany;

public class DeleteCompanyCommand : Command
{
	public DeleteCompanyCommand(Guid id, CompanyId companyId) : base(id)
	{
		CompanyId = companyId;
	}
	
	public CompanyId CompanyId { get; }
}