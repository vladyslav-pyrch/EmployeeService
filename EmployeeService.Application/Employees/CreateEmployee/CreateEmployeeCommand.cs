using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : Command<EmployeeId>
{
    public CreateEmployeeCommand(EmployeeId identity, EmployeeDto employeeDto) : base(identity)
    {
        EmployeeDto = employeeDto;
    }
    
    public EmployeeDto EmployeeDto { get; }
}