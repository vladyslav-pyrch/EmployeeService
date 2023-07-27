using EmployeeService.Common.Application.Commands;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IDomainEventDispatcher domainEventDispatcher)
    {
        _employeeRepository = employeeRepository;
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    public void Handle(CreateEmployeeCommand command)
    {
        Employee employee = new(command.Identity, command.EmployeeDto.Name, command.EmployeeDto.Surname,
            command.EmployeeDto.Passport, command.EmployeeDto.PhoneNumber, command.EmployeeDto.Workplace);
        
        _employeeRepository.AddEmployee(employee);
        _employeeRepository.Save();
        
        _domainEventDispatcher.Publish(new EmployeeCreated(nameof(Employee)));
    }
}