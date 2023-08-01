namespace EmployeeService.Common.Application.Commands;

public abstract class Command : ICommand
{
	protected Command(Guid id) => Id = id;

	public Guid Id { get; }
}

public abstract class Command<TResult> : ICommand<TResult>
{
	protected Command(Guid id) => Id = id;

	public Guid Id { get; }
}