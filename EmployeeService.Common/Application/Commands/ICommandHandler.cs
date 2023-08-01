namespace EmployeeService.Common.Application.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
	public void Handle(TCommand command);
}

public interface ICommandHandler<in TCommand, out TResult> where TCommand : ICommand<TResult>
{
	public TResult Handle(TCommand command);
}