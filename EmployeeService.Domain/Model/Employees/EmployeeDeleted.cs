using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record EmployeeDeleted : DomainEvent
{
	public EmployeeDeleted(string source, string version = "1.0", Guid? subscriberId = null)
		: base(source, version, subscriberId)
	{
	}
}