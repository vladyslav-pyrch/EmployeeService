using EmployeeService.Application.Employees.GetNewEmployeeId;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeId>
{
	private readonly IDomainEventDispatcher _domainEventDispatcher;
	private readonly IEmployeeRepository _employeeRepository;
	private readonly GetNewEmployeeIdQueryHandler _getNewEmployeeIdQueryHandler;

	public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
		IDomainEventDispatcher domainEventDispatcher,
		GetNewEmployeeIdQueryHandler getNewEmployeeIdQueryHandler)
	{
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
		_getNewEmployeeIdQueryHandler = getNewEmployeeIdQueryHandler;
	}

	public EmployeeId Handle(CreateEmployeeCommand command)
	{
		EmployeeId id = _getNewEmployeeIdQueryHandler.Handle(
			new GetNewEmployeeIdQuery()
		);

		(string name, string surname, Passport passport, PhoneNumber phoneNumber, Workplace workplace) = command;

		var employee = new Employee(id, name, surname, passport, phoneNumber, workplace);

		_employeeRepository.AddEmployee(employee);
		_employeeRepository.Save();

		_domainEventDispatcher.Publish(new EmployeeCreated(nameof(Employee)));

		return id;
	}
}