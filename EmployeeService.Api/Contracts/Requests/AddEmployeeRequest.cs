using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Contracts.Requests;

public record AddEmployeeRequest
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Surname { get; set; }
    
    [Required]
    public string? PassportType { get; set; }
    
    [Required]
    public string? PassportNumber { get; set; }
    
    [Required]
    public string? Company { get; set; }
    
    [Required]
    public string? Department { get; set; }
    
    [Required]
    public string? DepartmentPhone { get; set; }
}