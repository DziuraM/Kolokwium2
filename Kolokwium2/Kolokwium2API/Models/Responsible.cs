using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2API.Models;

public class Responsible
{
    [Key, Column(Order = 0)]
    public int BatchId { get; set; }
    
    [Key, Column(Order = 1)]
    public int EmployeeId { get; set; }
    
    [Required, MaxLength(100)]
    public string Role { get; set; }

    public SeedlingBatch Batch { get; set; }
    public Employee Employee { get; set; }
}