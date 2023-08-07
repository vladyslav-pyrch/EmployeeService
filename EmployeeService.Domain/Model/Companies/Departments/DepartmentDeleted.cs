using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies.Departments;

public record DepartmentDeleted : DomainEvent
{
	public DepartmentDeleted(string source, string version = "1.0", Guid? subscriberId = null) : base(source, version,
		subscriberId)
	{
	}
}