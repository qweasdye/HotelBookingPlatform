using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Hotels.Commands;
using HotelBookingPlatform.Core.Hotels.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelBookingPlatform.Application.Features.Dto;
using OpenQA.Selenium;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase 
{
    private readonly IMediator _mediator;

    public HotelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HotelDto>>> GetHotels([FromQuery] string query = "")
    {
        var hotels = await _mediator.Send(new SearchHotels { Query = query });

        var result = hotels.Select(h => new HotelDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            Address = h.Address != null ? new AddressDto
            {
                Street = h.Address.Street,
                City = h.Address.City,
                Country = h.Address.Country
            } : null,
            Rooms = h.Rooms?.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                PricePerNight = r.PricePerNight,
                Type = r.Type,
                Capacity = r.Capacity
            }).ToList() ?? new List<RoomDto>() // Если Rooms null, возвращаем пустой список
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")] 
    public async Task<ActionResult<Hotel>> GetById(int id)
    {
        var result = await _mediator.Send(new GetHotelById { Id = id });
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateHotel command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotel command)
    {
        try
        {
            if (id != command.Id)
            {
                return BadRequest("ID in route does not match ID in body");
            }

            await _mediator.Send(command);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteHotel { Id = id });
        return NoContent();
    }
}