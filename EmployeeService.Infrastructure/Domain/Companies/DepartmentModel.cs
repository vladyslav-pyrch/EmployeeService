namespace EmployeeService.Infrastructure.Domain.Companies;

internal class DepartmentModel
{
	public DepartmentModel(int id, string name, string phone, int companyId)
	{
		Id = id;
		Name = name;
		Phone = phone;
		CompanyId = companyId;
	}

	public int Id { get; set; }

	public string Name { get; set; }

	public string Phone { get; set; }

	public int CompanyId { get; set; }
}