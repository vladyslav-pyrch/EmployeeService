namespace EmployeeService.Common.Domain.Model;

public interface IEntity<out TId> where TId : IIdentity
{
    public TId Identity { get; }
}