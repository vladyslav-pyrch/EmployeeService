namespace EmployeeService.Infrastructure.DataAccess.Model;

public class Department
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Phone { get; set; }
    
    public int CompanyId { get; set; }
}