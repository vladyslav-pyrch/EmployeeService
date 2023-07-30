using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Common.Application.Commands;

public interface ICommand
{
    public Guid Id { get; }
}

public interface ICommand<out TResult>
{
    public Guid Id { get; }
}