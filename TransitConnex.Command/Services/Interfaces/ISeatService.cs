using TransitConnex.Domain.DTOs.Seat;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface ISeatService
    {
        Task<List<SeatDto>> GetAllSeats();
        
        Task<SeatDto> GetSeatById(Guid id);
        
        Task<bool> SeatExists(Guid id);
        
        Task<SeatDto> CreateSeat(SeatCreateDto seatDto);

        Task<SeatDto> EditSeat(Guid id, SeatCreateDto editedSeat);
        
        Task DeleteSeat(Guid id);
    }
}
