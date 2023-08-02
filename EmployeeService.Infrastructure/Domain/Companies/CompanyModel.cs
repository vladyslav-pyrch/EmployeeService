namespace EmployeeService.Infrastructure.Domain.Companies;

internal class CompanyModel
{
	public CompanyModel(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public int Id { get; set; }

	public string Name { get; set; }
}