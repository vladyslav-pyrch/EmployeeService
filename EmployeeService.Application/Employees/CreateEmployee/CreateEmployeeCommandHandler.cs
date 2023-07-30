using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeId>
{
    private readonly IIdentityFactory<EmployeeId> _identityFactory;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    public CreateEmployeeCommandHandler(IIdentityFactory<EmployeeId> identityFactory, IEmployeeRepository employeeRepository, IDomainEventDispatcher domainEventDispatcher)
    {
        _identityFactory = identityFactory;
        _employeeRepository = employeeRepository;
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    public EmployeeId Handle(CreateEmployeeCommand command)
    {
        EmployeeId id = _identityFactory.GenerateId();

        var (name, surname, passport, phoneNumber, workplace) = command.EmployeeDto;
        
        var employee = new Employee(id, name, surname, passport, phoneNumber, workplace);
        
        _employeeRepository.AddEmployee(employee);
        _employeeRepository.Save();
        
        _domainEventDispatcher.Publish(new EmployeeCreated(nameof(Employee)));

        return id;
    }
}