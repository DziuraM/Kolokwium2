using Kolokwium2API.DTOs;

namespace Kolokwium2API.Services;

public interface IDbService
{
    Task<NurseryDto> GetNurseryWithBatchesAsync(int id);
    Task AddBatchAsync(AddBatchDto dto);
}