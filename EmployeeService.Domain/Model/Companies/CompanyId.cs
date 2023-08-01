using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies;

public record CompanyId : Identity<int>
{
	public CompanyId(int id) : base(id)
	{
	}

	public override int Deconvert() => Convert.ToInt32(Id);
}