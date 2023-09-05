using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;
using Musicians.Domain.Entities;
using System.Security.Claims;

namespace Musicians.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MusiciansController : ControllerBase
{
    private readonly IMusicianService _musicianService;

    public MusiciansController(IMusicianService musicianService)
    {
        _musicianService = musicianService;
    }

    [HttpGet("{id}")]    
    public async Task<IActionResult> Get(int id)
    {
        var musician = await _musicianService.GetByIdAsync(id);

        if (musician == null)
        {
            return NotFound();
        }

        return Ok(musician);
    }

    [HttpGet]    
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _musicianService.GetAllAsync());        
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MusicianDto musicianDto)
    {
        var created = await _musicianService.CreateAsync(musicianDto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [Authorize]
    [HttpPut]    
    public async Task<IActionResult> Update([FromBody] Musician musician)
    {
        try
        {
            await _musicianService.UpdateAsync(musician);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpDelete("{id}")]    
    public async Task<IActionResult> Delete(int id)
    {        
        try
        {
            await _musicianService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}
