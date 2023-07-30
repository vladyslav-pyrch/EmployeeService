using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : Command<EmployeeId>
{
    public CreateEmployeeCommand(Guid id, EmployeeDto employeeDto) : base(id)
    {
        EmployeeDto = employeeDto;
    }
    
    public EmployeeDto EmployeeDto { get; }
}