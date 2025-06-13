namespace Kolokwium2API.Models;

using System.ComponentModel.DataAnnotations;

public class TreeSpecies
{
    [Key] 
    public int SpeciesId { get; set; }
    [Required, MaxLength(100)] 
    public string LatinName { get; set; }
    public int GrowthTimeInYears { get; set; }

    public ICollection<SeedlingBatch> Batches { get; set; }
}