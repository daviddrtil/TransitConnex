using System.Linq.Expressions;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Models;

namespace TransitConnex.Infrastructure.Mappings
{
    public static class IconMapper
    {
        public static readonly Expression<Func<Icon, IconDto>> AsDto = 
            item => new IconDto
            {
                Id = item.Id, 
                Name = item.Name, 
                Svg = item.Svg
            };
        
        public static IQueryable<IconDto> ToDto(this IQueryable<Icon> query)
        {
            return query.Select(AsDto);
        }
    }
}
