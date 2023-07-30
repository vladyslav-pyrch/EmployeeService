namespace EmployeeService.Infrastructure.Domain.Employees;

public class EmployeeModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Phone { get; set; }
    
    public int DepartmentId { get; set; }
    
    public int PassportId { get; set; }
}