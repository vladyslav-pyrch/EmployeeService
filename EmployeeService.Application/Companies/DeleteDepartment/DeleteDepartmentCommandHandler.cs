using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;

namespace EmployeeService.Application.Companies.DeleteDepartment;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand>
{
	private readonly IsThereDepartmentQueryHandler _isThereDepartmentQueryHandler;

	private readonly ICompanyRepository _companyRepository;

	private readonly IDomainEventDispatcher _domainEventDispatcher;

	public DeleteDepartmentCommandHandler(IsThereDepartmentQueryHandler isThereDepartmentQueryHandler,
		ICompanyRepository companyRepository,
		IDomainEventDispatcher domainEventDispatcher)
	{
		_isThereDepartmentQueryHandler = isThereDepartmentQueryHandler;
		_companyRepository = companyRepository;
		_domainEventDispatcher = domainEventDispatcher;
	}

	public void Handle(DeleteDepartmentCommand command)
	{
		CheckCommand(command);
		
		_companyRepository.DeleteDepartmentById(command.DepartmentId);
		
		_companyRepository.Save();
		
		_domainEventDispatcher.Publish(new DepartmentDeleted(nameof(Company)));
	}

	private void CheckCommand(DeleteDepartmentCommand command)
	{
		var isThereDepartmentQuery = new IsThereDepartmentQuery(command.DepartmentId);

		if (!_isThereDepartmentQueryHandler.Handle(isThereDepartmentQuery))
			throw new InvalidOperationException("There is no such department");
	}
}