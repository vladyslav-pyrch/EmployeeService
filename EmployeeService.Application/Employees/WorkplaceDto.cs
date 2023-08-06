namespace EmployeeService.Application.Employees;

internal record WorkplaceDto
{
	public int CompanyId { get; set; }
	
	public int DepartmentId { get; set; }
}