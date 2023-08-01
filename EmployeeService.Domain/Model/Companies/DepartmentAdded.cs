using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies;

public record DepartmentAdded : DomainEvent
{
	public DepartmentAdded(string source, string version = "1.0", Guid? subscriberId = null)
		: base(source, version, subscriberId)
	{
	}
}