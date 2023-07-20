namespace EmployeeService.Api.Contracts.Models;

public record Passport
{
    public string Type { get; set; }
    
    public string Number { get; set; }
}