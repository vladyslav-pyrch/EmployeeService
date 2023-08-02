namespace EmployeeService.Infrastructure.Domain.Employees;

internal class PassportTypeModel
{
	public PassportTypeModel(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public int Id { get; set; }

	public string Name { get; set; }
}