using EmployeeService.Common.Application.Commands;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Domain.Model.SharedKernel;

namespace EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : Command<EmployeeId>
{
	public CreateEmployeeCommand(Guid id, string name, string surname, Passport passport, PhoneNumber phoneNumber,
		Workplace workplace) : base(id)
	{
		Name = name;
		Surname = surname;
		Passport = passport;
		PhoneNumber = phoneNumber;
		Workplace = workplace;
	}

	public string Name { get; set; }

	public string Surname { get; set; }

	public Passport Passport { get; set; }

	public PhoneNumber PhoneNumber { get; set; }

	public Workplace Workplace { get; set; }

	public void Deconstruct(out string name, out string surname, out Passport passport, out PhoneNumber phoneNumber,
		out Workplace workplace)
	{
		name = Name;
		surname = Surname;
		passport = Passport;
		phoneNumber = PhoneNumber;
		workplace = Workplace;
	}
}