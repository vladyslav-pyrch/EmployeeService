using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record EmployeeCreated : DomainEvent
{
    public EmployeeCreated(string source, string version = "1.0", Guid? subscriberId = null) : base(source, version, subscriberId)
    {
    }
}