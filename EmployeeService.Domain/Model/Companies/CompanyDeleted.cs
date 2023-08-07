using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies;

public record CompanyDeleted : DomainEvent
{
	public CompanyDeleted(string source, string version = "1.0", Guid? subscriberId = null) : base(source, version,
		subscriberId)
	{
	}
}