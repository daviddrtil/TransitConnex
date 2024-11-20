using System.Linq.Expressions;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Models;

namespace TransitConnex.Domain.Mappings
{
    public static class VehicleMapper
    {
        public static readonly Expression<Func<Vehicle, VehicleDto>> AsDto =
            item => new VehicleDto
            {
                Id = item.Id,
                Capacity = item.Capacity,
                Label = item.Label, // TODO -> resolve this
                Manufacturer = item.Manufacturer,
                Spz = item.Spz,
                VehicleType = item.VehicleType,
                IconId = item.IconId,
                LineId = item.LineId,
            };
        
        public static IQueryable<VehicleDto> ToDto(this IQueryable<Vehicle> query)
        {
            return query.Select(AsDto);
        }
    }
}
