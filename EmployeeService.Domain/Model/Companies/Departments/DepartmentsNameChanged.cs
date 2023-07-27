using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies.Departments;

public record DepartmentsNameChanged : DomainEvent
{
    public DepartmentsNameChanged(string source, string version = "1.0", Guid? subscriberId = null) : base(source, version, subscriberId)
    {
    }
}