using TransitConnex.Domain.DTOs.Seat;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public Task<List<SeatDto>> GetAllSeats()
        {
            throw new NotImplementedException();
        }

        public Task<SeatDto> GetSeatById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SeatExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SeatDto> CreateSeat(SeatCreateDto seatDto)
        {
            throw new NotImplementedException();
        }

        public Task<SeatDto> EditSeat(Guid id, SeatCreateDto editedSeat)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSeat(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
