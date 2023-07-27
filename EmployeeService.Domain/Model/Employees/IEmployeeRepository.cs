using EmployeeService.Common.Domain.Model;

namespace EmployeeService.Domain.Model.Employees;

public interface IEmployeeRepository : IRepository<Employee, EmployeeId>
{
    public void AddEmployee(Employee employee);
}