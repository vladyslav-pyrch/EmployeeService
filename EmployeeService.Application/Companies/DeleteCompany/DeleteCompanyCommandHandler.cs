using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;

namespace EmployeeService.Application.Companies.DeleteCompany;

public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand>
{
	private readonly IsThereCompanyQueryHandler _isThereCompanyQueryHandler;

	private readonly ICompanyRepository _companyRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	public DeleteCompanyCommandHandler(IsThereCompanyQueryHandler isThereCompanyQueryHandler,
		ICompanyRepository companyRepository,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_isThereCompanyQueryHandler = isThereCompanyQueryHandler;
		_companyRepository = companyRepository;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public void Handle(DeleteCompanyCommand command)
	{
		CheckCommand(command);
		
		_companyRepository.DeleteCompanyById(command.CompanyId);
		
		_companyRepository.Save();
		
		_domainEventDispatcher.Publish(new CompanyDeleted(nameof(Company)));
	}

	private void CheckCommand(DeleteCompanyCommand command)
	{
		var isThereCompanyQuery = new IsThereCompanyQuery(command.CompanyId);

		if (!_isThereCompanyQueryHandler.Handle(isThereCompanyQuery))
			throw new InvalidOperationException("There is no such company");
	}
}