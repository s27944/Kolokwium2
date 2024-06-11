using Kolokwium.RequestModels;
using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class ApiController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public ApiController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    

    [HttpGet("characters/{id:int}")]
    public async Task<IActionResult> GetRequest(int id)
    {
        try
        {
            var ans = _databaseService.Get(id);
            return Ok(ans);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    
    [HttpPost("characters/{id:int}/backpackslots")]
    public async Task<IActionResult> PostRequest(int id, Request request)
    {
        try
        {
            var response = await _databaseService.Post(id, request);
            return Created(response);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
}