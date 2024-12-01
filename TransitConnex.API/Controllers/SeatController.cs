using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Query.Queries;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SeatController(SeatCommandHandler seatCommandHandler, SeatQueryHandler seatQueryHandlerSoT) : Controller
{
    [HttpPost("filter")]
    public async Task<ActionResult<List<SeatDto>>> GetSeatsFilteredSoT(SeatFilteredQuery filter)
    {
        return Ok(await seatQueryHandlerSoT.HandleGetSeatsFiltered(filter));
    }
    
    /// <summary>
    /// Endpoint for obtaining seats of scheduled route with their state - reserved/free.
    /// </summary>
    /// <param name="scheduledRouteId"></param>
    [HttpGet("route/{scheduledRouteId}/seats")]
    public async Task<ActionResult<List<SeatDto>>> GetSeatsForScheduledRouteWithState(Guid scheduledRouteId)
    {
        return Ok(await seatQueryHandlerSoT.HandleGetSeatsForScheduledRouteWithState(scheduledRouteId));
    }
    
    /// <summary>
    /// Endpoint for creating new single seat.
    /// </summary>
    /// <param name="createCommand">Command containing info about created seat.</param>
    /// <returns>Method status with id of created seat.</returns>
    [HttpPost]
    [AuthorizedByAdmin]
    public async Task<Guid> CreateSeat(SeatCreateCommand createCommand)
    {
        return await seatCommandHandler.HandleCreate(createCommand);
    }

    /// <summary>
    /// Endpoint for creating reservation on selected seats.
    /// </summary>
    /// <param name="reserveCommand">Command containing seats for reservation.</param>
    /// <returns>Method status.</returns>
    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveSeats(SeatReservationCommand reserveCommand)
    {
        await seatCommandHandler.HandleSeatReservation(reserveCommand);
        return Ok();
    }
    
    /// <summary>
    /// Endpoint for freeing reserved seats.
    /// </summary>
    /// <param name="freeCommand">Command containing seats to free.</param>
    /// <returns>Method status.</returns>
    [HttpPost("free")]
    public async Task<IActionResult> FreeSeats(SeatReservationCommand freeCommand)
    {
        await seatCommandHandler.HandleSeatFree(freeCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for editing seat.
    /// </summary>
    /// <param name="updateCommand">Command containing edited seat info.</param>
    /// <returns>Method status.</returns>
    [HttpPut]
    [AuthorizedByAdmin]
    public async Task<IActionResult> EditSeat(SeatUpdateCommand updateCommand)
    {
        await seatCommandHandler.HandleUpdate(updateCommand);
        return Ok();
    }

    /// <summary>
    /// Endpoint for deleting single seat by id - in case seat was destroyed for example.
    ///
    /// All tickets linked with this seat will be moved to another or if none free ticket will be refunded.
    /// </summary>
    /// <param name="id">Id of deleted seat.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [AuthorizedByAdmin]
    public async Task<IActionResult> DeleteSeat(Guid id)
    {
        await seatCommandHandler.HandleDelete(id);
        return Ok();
    }
}
