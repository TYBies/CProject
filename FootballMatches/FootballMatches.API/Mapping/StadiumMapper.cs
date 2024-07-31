using FootballMatches.API.DTOs;
using FootballMatches.API.Models;

namespace FootballMatches.API.Mapping
{
    public class StadiumMapper : IMapper<Stadium, StadiumDto>
    {
        public StadiumDto MapToDto(Stadium entity)
        {
            return new StadiumDto
            {
                StadiumName = entity.StadiumName,
                StadiumId = entity.StadiumId,
            };
        }

        public IEnumerable<StadiumDto> MapToDtos(IEnumerable<Stadium> entities)
        {
            foreach (var entity in entities)
            {
                yield return MapToDto(entity);
            }
        }

        public IEnumerable<Stadium> MapToEntities(IEnumerable<StadiumDto> dtos)
        {
            throw new NotImplementedException();
        }

        public Stadium MapToEntity(StadiumDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
