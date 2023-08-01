using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeId>
{
	private readonly IDomainEventDispatcher _domainEventDispatcher;
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IIdentityFactory<EmployeeId> _identityFactory;

	public CreateEmployeeCommandHandler(IIdentityFactory<EmployeeId> identityFactory,
		IEmployeeRepository employeeRepository, IDomainEventDispatcher domainEventDispatcher)
	{
		_identityFactory = identityFactory;
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public EmployeeId Handle(CreateEmployeeCommand command)
	{
		EmployeeId id = _identityFactory.GenerateId();

		(string name, string surname, Passport passport, PhoneNumber phoneNumber, Workplace workplace) = command;

		var employee = new Employee(id, name, surname, passport, phoneNumber, workplace);

		_employeeRepository.AddEmployee(employee);
		_employeeRepository.Save();

		_domainEventDispatcher.Publish(new EmployeeCreated(nameof(Employee)));

		return id;
	}
}