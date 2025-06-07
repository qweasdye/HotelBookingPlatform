using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Hotels.Commands;
using HotelBookingPlatform.Core.Hotels.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase // FIX ALL
{
    private readonly IMediator _mediator;

    public HotelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetById(int id)
    {
        var query = new GetHotelById { Id = id };
        var hotel = await _mediator.Send(query);

        if (hotel == null) return NotFound();
        return Ok(hotel);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Hotel>>> Search([FromQuery] string query)
    {
        var searchQuery = new SearchHotels { Query = query };
        var hotels = await _mediator.Send(searchQuery);
        return Ok(hotels);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateHotel command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Вернёт конкретные ошибки
        }

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHotel command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteHotel { Id = id });
        return NoContent();
    }
}