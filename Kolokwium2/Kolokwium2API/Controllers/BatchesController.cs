using Kolokwium2API.DTOs;
using Kolokwium2API.Exceptions;
using Kolokwium2API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2API.Controllers;

[ApiController]
[Route("api/batches")]
public class BatchesController : ControllerBase
{
    private readonly IDbService _service;

    public BatchesController(IDbService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddBatch([FromBody] AddBatchDto dto)
    {
        try
        {
            await _service.AddBatchAsync(dto);
            return Created(string.Empty, null);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
