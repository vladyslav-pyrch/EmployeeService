namespace EmployeeService.Application.Employees;

internal record PassportDto
{
	public string Number { get; set; }
	
	public string Type { get; set; }
}