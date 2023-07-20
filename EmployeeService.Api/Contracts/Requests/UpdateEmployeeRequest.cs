using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Contracts.Requests;

public record UpdateEmployeeRequest
{
    [Required]
    public int? EmployeeId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public string? PassportType { get; set; }
    
    public string? PassportNumber { get; set; }
    
    public string? Company { get; set; }
    
    public string? Department { get; set; }
    
    public string? DepartmentPhone { get; set; }
}