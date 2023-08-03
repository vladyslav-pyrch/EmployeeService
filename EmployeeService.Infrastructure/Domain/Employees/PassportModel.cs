namespace EmployeeService.Infrastructure.Domain.Employees;

internal class PassportModel
{
	public int Id { get; set; }

	public string Number { get; set; }

	public int PassportTypeId { get; set; }
}