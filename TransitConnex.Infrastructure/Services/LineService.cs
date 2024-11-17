using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class LineService : ILineService
    {
        private readonly ILineRepository _lineRepository;

        public LineService(ILineRepository lineRepository)
        {
            _lineRepository = lineRepository;
        }

        public Task<List<LineDto>> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public Task<LineDto> GetLineById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LineExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<LineDto> CreateLine(LineCreateDto lineDto)
        {
            throw new NotImplementedException();
        }

        public Task<LineDto> EditLine(Guid id, LineCreateDto editedLine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLine(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
