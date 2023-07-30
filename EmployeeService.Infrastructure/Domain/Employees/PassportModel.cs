namespace EmployeeService.Infrastructure.Domain.Employees;

public class PassportModel
{
    public int Id { get; set; }
    
    public string Number { get; set; }
    
    public int PassportTypeId { get; set; }
}