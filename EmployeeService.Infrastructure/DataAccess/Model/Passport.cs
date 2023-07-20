namespace EmployeeService.Infrastructure.DataAccess.Model;

public class Passport
{
    public int Id { get; set; }
    
    public string Number { get; set; }
    
    public int PassportTypeId { get; set; }
}