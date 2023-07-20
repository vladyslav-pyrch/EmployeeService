namespace EmployeeService.Api.Contracts.Models;

public record Employee
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }
    
    public string Phone { get; set; }
    
    public int CompanyId { get; set; }
    
    public Passport Passport { get; set; }
    
    public Department Department { get; set; }
}