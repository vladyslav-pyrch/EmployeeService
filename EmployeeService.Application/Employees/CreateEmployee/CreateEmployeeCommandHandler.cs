using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Application.Companies.IsThereDepartmentInCompany;
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

	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly IsThereDepartmentInCompanyQueryHandler _isThereDepartmentInCompanyQueryHandler;

	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
		IDomainEventDispatcher domainEventDispatcher,
		GetNewEmployeeIdQueryHandler getNewEmployeeIdQueryHandler,
		IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		IsThereDepartmentQueryHandler isThereDepartmentQueryHandler,
		IsThereDepartmentInCompanyQueryHandler isThereDepartmentInCompanyQueryHandler)
	{
		_employeeRepository = employeeRepository;
		_domainEventDispatcher = domainEventDispatcher;
		_getNewEmployeeIdQueryHandler = getNewEmployeeIdQueryHandler;
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
		_isThereDepartmentInCompanyQueryHandler = isThereDepartmentInCompanyQueryHandler;
	}

	public EmployeeId Handle(CreateEmployeeCommand command)
	{
		CheckCommand(command);

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

	private void CheckCommand(CreateEmployeeCommand command)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(command.Workplace.Company);
		var isThereDepartmentQuery = new IsThereDepartmentQuery(command.Workplace.Department);
		var isThereDepartmentInCompanyQuery = new IsThereDepartmentInCompanyQuery(
			command.Workplace.Company, command.Workplace.Department
		);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
		{
			throw new InvalidOperationException(
				"There is no such company with such id"
			);
		}

		if (!_isThereDepartmentQueryHandler.Handle(isThereDepartmentQuery))
		{
			throw new InvalidOperationException(
				"There is no such department"
			);
		}

		if (!_isThereDepartmentInCompanyQueryHandler.Handle(isThereDepartmentInCompanyQuery))
		{
			throw new InvalidOperationException(
				"There is no such department in the company"
			);
		}
	}
}