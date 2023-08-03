using EmployeeService.Domain.Model.Companies;
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

		IEnumerable<DepartmentModel> departmentModels = company.Departments.Select(department =>
			new DepartmentModel(
				department.Identity.Deconvert(),
				department.Name,
				department.PhoneNumber.Number,
				department.CompanyId.Deconvert()
			)
		);
		
		_dbContext.AddRange(departmentModels);
	}
	
	public void Save() => _dbContext.SaveChanges();
}