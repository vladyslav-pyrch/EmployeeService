using EmployeeService.Application.Employees.GetEmployeeById;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
	private readonly IsThereEmployeeQueryHandler _isThereEmployeeQueryHandler;

	private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;

	private readonly IEmployeeRepository _employeeRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	public UpdateEmployeeCommandHandler(IsThereEmployeeQueryHandler isThereEmployeeQueryHandler,
		GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler,
		IEmployeeRepository employeeRepository,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_isThereEmployeeQueryHandler = isThereEmployeeQueryHandler;
		_getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public void Handle(UpdateEmployeeCommand command)
	{
		CheckCommand(command);

		Employee employee = _getEmployeeByIdQueryHandler.Handle(
			new GetEmployeeByIdQuery(command.EmployeeId)
		);
		
		UpdateEmployee(command, employee);

		_employeeRepository.UpdateEmployee(employee);
		
		_employeeRepository.Save();
		
		foreach (IDomainEvent employeeDomainEvent in employee.DomainEvents)
			_domainEventDispatcher.Add(employeeDomainEvent);
		
		_domainEventDispatcher.Commit<IDomainEvent>();
	}
	
	private void CheckCommand(UpdateEmployeeCommand command)
	{
		var isThereEmployeeQuery = new IsThereEmployeeQuery(command.EmployeeId);

		if (!_isThereEmployeeQueryHandler.Handle(isThereEmployeeQuery))
			throw new InvalidOperationException("There is no such employee.");
	}
	
	private static void UpdateEmployee(UpdateEmployeeCommand command, Employee employee)
	{
		if (command.Name != null)
			employee.ChangeName(command.Name);
		if (command.Surname != null)
			employee.ChangeSurname(command.Surname);
		if (command.Passport != null)
			employee.ChangePassport(command.Passport);
		if (command.PhoneNumber != null) 
			employee.ChangePhoneNumber(command.PhoneNumber);
		if (command.Workplace != null && command.Workplace.Company == employee.Workplace.Company) // ToDo Add a check weather department is in Company
			employee.ChangeDepartment(command.Workplace.Department);
		else if (command.Workplace != null)
			employee.ChangeWorkplace(command.Workplace);
	}
}