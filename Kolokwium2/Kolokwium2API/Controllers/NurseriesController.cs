using Kolokwium2API.Services;
using Kolokwium2API.Exceptions;

namespace Kolokwium2API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/nurseries")]
public class NurseriesController : ControllerBase
{
    private readonly IDbService _service;

    public NurseriesController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetBatches(int id)
    {
        try
        {
            var result = await _service.GetNurseryWithBatchesAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

