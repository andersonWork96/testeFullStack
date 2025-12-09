using Microsoft.AspNetCore.Mvc;
using Server.Application.Contracts;
using Server.Application.Dtos;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDto>> GetById(int id)
    {
        var result = await _genreService.GetByIdAsync(id);
        if (result.IsNotFound)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> Create([FromBody] CreateGenreRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _genreService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GenreDto>> Update(int id, [FromBody] UpdateGenreRequest request)
    {
        var result = await _genreService.UpdateAsync(id, request);
        if (result.IsNotFound)
        {
            return NotFound();
        }

        if (result.IsInvalid)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _genreService.DeleteAsync(id);
        if (result.IsNotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}
