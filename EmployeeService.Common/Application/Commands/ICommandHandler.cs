using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Common.Application.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand<IIdentity>
{
    public void Handle(TCommand command);
}

public interface ICommandHandler<in TCommand, out TResult> where TCommand : ICommand<IIdentity, TResult>
{
    public TResult Handle(TCommand command);
}