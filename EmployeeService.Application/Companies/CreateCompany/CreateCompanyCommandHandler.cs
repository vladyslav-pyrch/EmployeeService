using EmployeeService.Application.Companies.GetNewCompanyId;
using EmployeeService.Application.Companies.GetNewDepartmentId;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Companies.CreateCompany;

public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyId>
{
	private readonly ICompanyRepository _companyRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	private readonly GetNewCompanyIdQueryHandler _getNewCompanyIdQueryHandler;

	private readonly GetNewDepartmentIdQueryHandler _getNewDepartmentIdQueryHandler;

	public CreateCompanyCommandHandler(ICompanyRepository companyRepository,
		IDomainEventDispatcher domainEventDispatcher,
		GetNewCompanyIdQueryHandler getNewCompanyIdQueryHandler,
		GetNewDepartmentIdQueryHandler getNewDepartmentIdQueryHandler)
	{
		_companyRepository = companyRepository;
		_domainEventDispatcher = domainEventDispatcher;
		_getNewCompanyIdQueryHandler = getNewCompanyIdQueryHandler;
		_getNewDepartmentIdQueryHandler = getNewDepartmentIdQueryHandler;
	}

	private static string MainDepartmentName => "Main Department";

	public CompanyId Handle(CreateCompanyCommand command)
	{
		CompanyId companyId = _getNewCompanyIdQueryHandler.Handle(
			new GetNewCompanyIdQuery()
		);
		DepartmentId departmentId = _getNewDepartmentIdQueryHandler.Handle(
			new GetNewDepartmentIdQuery()
		);

		var mainDepartment = new List<Department>
		{
			new(departmentId,
				MainDepartmentName,
				new PhoneNumber(command.PhoneNumber),
				companyId)
		};

		var company = new Company(companyId, command.Name, mainDepartment);

		_companyRepository.CreateCompany(company);
		_companyRepository.Save();
		
		_domainEventDispatcher.Publish(new CompanyCreated(nameof(Company)));

		return companyId;
	}
}