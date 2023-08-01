namespace EmployeeService.Infrastructure.Domain.Companies;

public class DepartmentModel
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string Phone { get; set; }

	public int CompanyId { get; set; }
}