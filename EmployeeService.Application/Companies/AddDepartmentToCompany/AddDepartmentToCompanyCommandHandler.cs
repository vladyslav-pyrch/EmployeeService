using EmployeeService.Application.Companies.GetCompanyById;
using EmployeeService.Application.Companies.GetNewDepartmentId;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.AddDepartmentToCompany;

public class AddDepartmentToCompanyCommandHandler : ICommandHandler<AddDepartmentToCompanyCommand, DepartmentId>
{
	private readonly ICompanyRepository _companyRepository;

	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly GetCompanyByIdQueryHandler _getCompanyByIdQueryHandler;

	private readonly GetNewDepartmentIdQueryHandler _getNewDepartmentIdQueryHandler;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	public AddDepartmentToCompanyCommandHandler(ICompanyRepository companyRepository,
		IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		GetCompanyByIdQueryHandler getCompanyByIdQueryHandler,
		GetNewDepartmentIdQueryHandler getNewDepartmentIdQueryHandler,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_companyRepository = companyRepository;
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_getCompanyByIdQueryHandler = getCompanyByIdQueryHandler;
		_getNewDepartmentIdQueryHandler = getNewDepartmentIdQueryHandler;
		_domainEventDispatcher = domainEventDispatcher;
	}


	public DepartmentId Handle(AddDepartmentToCompanyCommand command)
	{
		CheckCommand(command);

		DepartmentId departmentId = _getNewDepartmentIdQueryHandler.Handle(
			new GetNewDepartmentIdQuery()
		);

		Company company = _getCompanyByIdQueryHandler.Handle(
			new GetCompanyByIdQuery(command.CompanyId)
		);

		Department department = company.AddDepartment(
			departmentId,
			command.DepartmentName,
			command.DepartmentPhoneNumber
		);

		_companyRepository.AddDepartment(department);
		
		_companyRepository.Save();

		foreach (IDomainEvent companyDomainEvent in company.DomainEvents)
			_domainEventDispatcher.Publish(companyDomainEvent);

		return department.Identity;
	}

	private void CheckCommand(AddDepartmentToCompanyCommand command)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(command.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company.");
	}
}