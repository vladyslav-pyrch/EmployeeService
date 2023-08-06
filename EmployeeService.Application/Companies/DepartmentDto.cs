namespace EmployeeService.Application.Companies;

internal record DepartmentDto
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string PhoneNumber { get; set; }

	public int CompanyId { get; set; }
}