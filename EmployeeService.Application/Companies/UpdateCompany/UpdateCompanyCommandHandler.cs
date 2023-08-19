using EmployeeService.Application.Companies.GetCompanyById;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.UpdateCompany;

public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand>
{
	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly GetCompanyByIdQueryHandler _getCompanyByIdQueryHandler;

	private readonly ICompanyRepository _companyRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	public UpdateCompanyCommandHandler(IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		ICompanyRepository companyRepository,
		GetCompanyByIdQueryHandler getCompanyByIdQueryHandler,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_companyRepository = companyRepository;
		_getCompanyByIdQueryHandler = getCompanyByIdQueryHandler;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public void Handle(UpdateCompanyCommand command)
	{
		CheckCommand(command);

		Company company = _getCompanyByIdQueryHandler.Handle(
			new GetCompanyByIdQuery(command.CompanyId)
		);
		
		if (command.Name != null)
			company.ChangeName(command.Name);
		
		_companyRepository.UpdateCompany(company);
		
		_companyRepository.Save();

		foreach (IDomainEvent domainEvent in company.DomainEvents)
			_domainEventDispatcher.Add(domainEvent);
		
		_domainEventDispatcher.Commit<IDomainEvent>();
	}

	private void CheckCommand(UpdateCompanyCommand command)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(command.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company");
	}
}