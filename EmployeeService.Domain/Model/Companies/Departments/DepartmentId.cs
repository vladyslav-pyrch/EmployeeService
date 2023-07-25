using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Companies.Departments;

public record DepartmentId : Identity<int>
{
    public DepartmentId(int id) : base(id)
    {
    }

    public override int Deconvert()
    {
        return Convert.ToInt32(Id);
    }
}