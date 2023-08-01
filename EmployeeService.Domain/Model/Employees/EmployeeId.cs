using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public record EmployeeId : Identity<int>
{
	public EmployeeId(int id) : base(id)
	{
	}

	public override int Deconvert() => Convert.ToInt32(Id);
}