namespace EmployeeService.Infrastructure.Domain.Employees;

internal class EmployeeModel
{
	public EmployeeModel(int id, string name, string surname, string phone, int departmentId, int passportId)
	{
		Id = id;
		Name = name;
		Surname = surname;
		Phone = phone;
		DepartmentId = departmentId;
		PassportId = passportId;
	}

	public int Id { get; set; }

	public string Name { get; set; }

	public string Surname { get; set; }

	public string Phone { get; set; }

	public int DepartmentId { get; set; }

	public int PassportId { get; set; }
}