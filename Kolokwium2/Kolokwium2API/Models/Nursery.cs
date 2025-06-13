namespace Kolokwium2API.Models;

using System.ComponentModel.DataAnnotations;

public class Nursery
{
    [Key] 
    public int NurseryId { get; set; }
    
    [Required, MaxLength(100)] 
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }

    public ICollection<SeedlingBatch> SeedlingBatches { get; set; }
}


