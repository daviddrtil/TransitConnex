using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Domain.DTOs.Seat;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SeatController(SeatCommandHandler seatCommandHandler, SeatQueryHandler seatQueryHandlerSoT) : Controller
{
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

    // /// <summary> // TODO -> not supported for first release
    // /// Endpoint for creating multiple seats.
    // /// </summary>
    // /// <param name="createCommands">List of commands for creating seat.</param>
    // /// <returns>Method status with list of ids of created seats.</returns>
    // [HttpPost("batch")]
    // [AuthorizedByAdmin]
    // public async Task<List<Guid>> CreateSeats(List<SeatCreateCommand> createCommands)
    // {
    // }

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

    // /// <summary> // TODO -> not supported for first release
    // /// Endpoint for deleting seats of given vehicle.
    // ///
    // /// If vagon number is selected only seats from certain vagon are deleted.
    // /// If seat numbers are selected only seats with given numbers are deleted.
    // /// Seat numbers and vagon number are intersected.
    // /// </summary>
    // /// <param name="deleteCommand">Command obtaining info about from which vehicle and what seats to delete.</param>
    // /// <returns>Method status.</returns>
    // [HttpDelete("batch")]
    // [AuthorizedByAdmin]
    // public async Task<IActionResult> DeleteVehicleSeats(SeatDeleteCommand deleteCommand)
    // {
    //     // await SeatCommandHandler.HandleDelete(deleteCommand);
    //
    //     return Ok();
    // }
}
