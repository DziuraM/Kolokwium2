namespace Kolokwium2API.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SeedlingBatch
{
    [Key] 
    public int BatchId { get; set; }

    [ForeignKey("Nursery")] 
    public int NurseryId { get; set; }
    public Nursery Nursery { get; set; }

    [ForeignKey("TreeSpecies")] 
    public int SpeciesId { get; set; }
    public TreeSpecies TreeSpecies { get; set; }

    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }

    public ICollection<Responsible> Responsibles { get; set; }
}