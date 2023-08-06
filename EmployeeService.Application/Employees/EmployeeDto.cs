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
}