using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.CreateCompany;

public class CreateCompanyCommand : Command<CompanyId>
{
	public CreateCompanyCommand(Guid id, string name, string phoneNumber) : base(id)
	{
		Name = name;
		PhoneNumber = phoneNumber;
	}

	public string Name { get; }

	public string PhoneNumber { get; }
}