using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record EmployeesNameChanged : DomainEvent
{
	public EmployeesNameChanged(string source, string version = "1.0", Guid? subscriberId = null)
		: base(source, version, subscriberId)
	{
	}
}