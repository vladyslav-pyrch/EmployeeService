using EmployeeService.Application.Companies;
using EmployeeService.Application.Employees;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application;

internal static class Convert
{
	public static Employee ToEmployee(EmployeeDto employeeDto)
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

	public static Department ToDepartment(DepartmentDto departmentDto)
	{
		var id = new DepartmentId(departmentDto.Id);
		string name = departmentDto.Name;
		var phoneNumber = new PhoneNumber(departmentDto.PhoneNumber);
		var companyId = new CompanyId(departmentDto.CompanyId);

		return new Department(id, name, phoneNumber, companyId);
	}

	public static Workplace ToWorkplace(WorkplaceDto workplaceDto)
	{
		var companyId = new CompanyId(workplaceDto.CompanyId);
		var departmentId = new DepartmentId(workplaceDto.DepartmentId);

		return new Workplace(companyId, departmentId);
	}

	public static Passport ToPassport(PassportDto passportDto)
	{
		var passportNumber = new PassportNumber(passportDto.Number);
		var passportType = new PassportType(passportDto.Type);

		return new Passport(passportNumber, passportType);
	}
}