using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.DeleteEmployee;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
	private readonly IDomainEventDispatcher _domainEventDispatcher;
	private readonly IEmployeeRepository _employeeRepository;

	public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public void Handle(DeleteEmployeeCommand command)
	{
		_employeeRepository.DeleteById(command.EmployeeId);

		_domainEventDispatcher.Publish(new EmployeeDeleted(nameof(Employee)));
	}
}