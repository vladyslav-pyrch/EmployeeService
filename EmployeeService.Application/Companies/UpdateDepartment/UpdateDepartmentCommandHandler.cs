using EmployeeService.Application.Companies.GetDepartmentById;
using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.UpdateDepartment;

public class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand>
{
	private readonly GetDepartmentByIdQueryHandler _getDepartmentByIdQueryHandler;
	
	private readonly ICompanyRepository _companyRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;
	
	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	public UpdateDepartmentCommandHandler(ICompanyRepository companyRepository,
		IDomainEventDispatcher domainEventDispatcher,
		IsThereDepartmentQueryHandler isThereDepartmentQueryHandler,
		GetDepartmentByIdQueryHandler getDepartmentByIdQueryHandler)
	{
		_companyRepository = companyRepository;
		_domainEventDispatcher = domainEventDispatcher;
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
		_getDepartmentByIdQueryHandler = getDepartmentByIdQueryHandler;
	}

	public void Handle(UpdateDepartmentCommand command)
	{
		CheckCommand(command);

		Department department = _getDepartmentByIdQueryHandler.Handle(
			new GetDepartmentByIdQuery(command.DepartmentId)
		);
		
		if (command.Name != null)
			department.ChangeName(command.Name);
		if (command.PhoneNumber != null)
			department.ChangePhoneNumber(command.PhoneNumber);
        
		_companyRepository.UpdateDepartment(department);
		
		_companyRepository.Save();

		foreach (IDomainEvent domainEvent in department.DomainEvents)
			_domainEventDispatcher.Add(domainEvent);
		
		_domainEventDispatcher.Commit<IDomainEvent>();
	}

	private void CheckCommand(UpdateDepartmentCommand command)
	{
		var isThereDepartmentQuery = new IsThereDepartmentQuery(command.DepartmentId);

		if (!_isThereDepartmentQueryHandler.Handle(isThereDepartmentQuery))
			throw new InvalidOperationException("There is no such department");
	}
}