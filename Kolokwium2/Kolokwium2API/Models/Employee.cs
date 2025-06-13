using System.ComponentModel.DataAnnotations;

namespace Kolokwium2API.Models;

public class Employee
{
    [Key] 
    public int EmployeeId { get; set; }
    
    [Required, MaxLength(100)] 
    public string FirstName { get; set; }
    
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }

    public ICollection<Responsible> Responsibles { get; set; }
}