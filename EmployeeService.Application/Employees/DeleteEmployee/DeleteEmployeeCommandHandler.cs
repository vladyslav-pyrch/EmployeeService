using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.DeleteEmployee;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
	private readonly IDomainEventDispatcher _domainEventDispatcher;
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
		IDomainEventDispatcher domainEventDispatcher,
		IsThereEmployeeQueryHandler isThereEmployeeQueryHandler)
	{
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
	}

	public void Handle(DeleteEmployeeCommand command)
	{
		CheckCommand(command);
		
		_employeeRepository.DeleteById(command.EmployeeId);
		
		_employeeRepository.Save();

		_domainEventDispatcher.Publish(new EmployeeDeleted(nameof(Employee)));
	}

	private void CheckCommand(DeleteEmployeeCommand command)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(command.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee.");
	}
}