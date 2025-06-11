using HotelBookingPlatform.Application.Features.Bookings.Commands;
using HotelBookingPlatform.Application.Features.Bookings.Queries;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetAllBookings([FromQuery] string? searchQuery)
        {
            var query = new GetAllBookings(searchQuery);
            var bookings = await _mediator.Send(query);
            return Ok(bookings);
        }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var query = new GetBookingById(id);
            var booking = await _mediator.Send(query);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBooking request)
        {
            try
            {
                var booking = await _mediator.Send(request);
                return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var command = new DeleteBooking(id);
            await _mediator.Send(command);
            return NoContent();
        }

        // GET: api/bookings/room/5?checkIn=2023-01-01&checkOut=2023-01-10
        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<BookingResult>> GetBookingsForRoom(
            int roomId,
            [FromQuery] DateTime? checkIn,
            [FromQuery] DateTime? checkOut)
        {
            var query = new GetBookingsForRoom(roomId, checkIn, checkOut);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
