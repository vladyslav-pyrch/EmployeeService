using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.CreateEmployee;

public record EmployeeDto(string Name, string Surname, Passport Passport, PhoneNumber PhoneNumber, Workplace Workplace);