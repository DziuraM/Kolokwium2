namespace Kolokwium2API.DTOs;

public class NurseryDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public List<SeedlingBatchDto> Batches { get; set; }
    
}