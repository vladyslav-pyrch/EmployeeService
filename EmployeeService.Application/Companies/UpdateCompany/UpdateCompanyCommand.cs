using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.UpdateCompany;

public class UpdateCompanyCommand : Command
{
	public UpdateCompanyCommand(Guid id, CompanyId companyId) : base(id)
	{
		CompanyId = companyId;
	}
	
	public CompanyId CompanyId { get; set; }
	
	public string? Name { get; set; }
}