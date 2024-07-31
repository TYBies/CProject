using FootballMatches.API.DTOs;
using FootballMatches.API.Models;
using System.Reflection.Metadata.Ecma335;

namespace FootballMatches.API.Mapping
{
    public class TeamMapper : IMapper<Team, TeamDto>
    {
        public TeamDto MapToDto(Team entity)
        {

            return new TeamDto
            {
                TeamId = entity.TeamId,
                TeamName = entity.TeamName,
            };
        }

        public IEnumerable<TeamDto> MapToDtos(IEnumerable<Team> entities)
        {
            foreach (var entity in entities)
            {
                yield return MapToDto(entity);
            }
        }

        public IEnumerable<Team> MapToEntities(IEnumerable<TeamDto> dtos)
        {
            throw new NotImplementedException();
        }

        public Team MapToEntity(TeamDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
