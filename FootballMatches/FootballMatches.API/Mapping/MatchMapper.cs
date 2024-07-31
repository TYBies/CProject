using FootballMatches.API.DTOs;
using FootballMatches.API.Models;

namespace FootballMatches.API.Mapping
{
    public class MatchMaper : IMapper<Match, MatchDto>
    {
        private readonly IMapper<Stadium, StadiumDto> _stadiumMapper;
        private readonly IMapper<Team, TeamDto> _teamMapper;

        public MatchMaper(

        IMapper<Stadium, StadiumDto> stadiumMapper,
        IMapper<Team, TeamDto> teamMapper)
        {
            _stadiumMapper = stadiumMapper;
            _teamMapper = teamMapper;
        }
        public MatchDto MapToDto(Match entity)
        {

            return new MatchDto
            {
                MatchId = entity.MatchId,
                MatchDay = entity.MatchDay,
                KickoffTime = entity.PlannedKickoffTime,
                CompetitionId = entity.CompetitionId,
                DLProviderId = entity.DLProviderId,
                StadiumDto = entity.Stadium != null ? _stadiumMapper.MapToDto(entity.Stadium) : null,
                HomeTeamDto = entity.HomeTeam != null ? _teamMapper.MapToDto(entity.HomeTeam) : null,
                AwayTeamDto = entity.AwayTeam != null ? _teamMapper.MapToDto(entity.AwayTeam) : null
            };
        }

        public IEnumerable<MatchDto> MapToDtos(IEnumerable<Match> entities)
        {

            foreach (var entity in entities)
            {
                yield return MapToDto(entity);
            }
        }

        public IEnumerable<Match> MapToEntities(IEnumerable<MatchDto> dtos)
        {
            throw new NotImplementedException();
        }

        public Match MapToEntity(MatchDto dto)
        {
            throw new NotImplementedException();
        }
    }
}