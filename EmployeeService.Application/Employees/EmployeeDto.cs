using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees;

internal record EmployeeDto
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string Surname { get; set; }

	public string PhoneNumber { get; set; }

	public string PassportNumber { get; set; }

	public string PassportType { get; set; }

	public int DepartmentId { get; set; }

	public int CompanyId { get; set; }

	public static explicit operator Employee(EmployeeDto employeeDto)
	{
		var id = new EmployeeId(employeeDto.Id);
		string name = employeeDto.Name;
		string surname = employeeDto.Surname;
		var passport = new Passport(
			new PassportNumber(employeeDto.PassportNumber),
			new PassportType(employeeDto.PassportType)
		);
		var phoneNumber = new PhoneNumber(employeeDto.PhoneNumber);
		var workplace = new Workplace(
			new CompanyId(employeeDto.CompanyId),
			new DepartmentId(employeeDto.DepartmentId)
		);

		return new Employee(id, name, surname, passport, phoneNumber, workplace);
	}
}