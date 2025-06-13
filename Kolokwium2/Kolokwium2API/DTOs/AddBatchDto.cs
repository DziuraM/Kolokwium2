namespace Kolokwium2API.DTOs;

public class AddBatchDto
{
    public int Quantity { get; set; }
    public string Species { get; set; }
    public string Nursery { get; set; }
    public List<AddResponsibleDto> Responsibles { get; set; }
}