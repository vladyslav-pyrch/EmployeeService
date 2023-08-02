using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Infrastructure.DataAccess;

namespace EmployeeService.Infrastructure.Domain.Companies;

public class CompanyRepository : ICompanyRepository
{
	private readonly EmployeeServiceDbContext _dbContext;

	public CompanyRepository(EmployeeServiceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void CreateCompany(Company company)
	{
		var companyModel = new CompanyModel(company.Identity.Deconvert(), company.Name);

		_dbContext.Companies.Add(companyModel);

		foreach (Department department in company.Departments)
		{
			_dbContext.Departments.Add(new DepartmentModel(
				department.Identity.Deconvert(),
				department.Name,
				department.PhoneNumber.Number,
				department.CompanyId.Deconvert()
			));
		}
	}
	
	public void Save() => _dbContext.SaveChanges();
}