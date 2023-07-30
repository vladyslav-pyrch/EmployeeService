using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public interface IEmployeeRepository : IRepository
{
    public void AddEmployee(Employee employee);

    public void DeleteById(EmployeeId employeeId);
}