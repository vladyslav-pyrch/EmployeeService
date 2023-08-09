namespace EmployeeService.Application.Companies;

internal record CompanyDto
{
	public int Id { get; set; }
	
	public string Name { get; set; }
}