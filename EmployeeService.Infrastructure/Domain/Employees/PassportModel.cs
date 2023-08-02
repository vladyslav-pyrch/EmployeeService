namespace EmployeeService.Infrastructure.Domain.Employees;

internal class PassportModel
{
	public PassportModel(int id, string number, int passportTypeId)
	{
		Id = id;
		Number = number;
		PassportTypeId = passportTypeId;
	}

	public int Id { get; set; }

	public string Number { get; set; }

	public int PassportTypeId { get; set; }
}