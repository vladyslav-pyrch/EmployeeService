using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Common.Application.Commands;

public interface ICommand<out TIdentity> where TIdentity : IIdentity
{
    public TIdentity Identity { get; }
}

public interface ICommand<out TIdentity, out TResult> where TIdentity : IIdentity
{
    public TIdentity Identity { get; }
}