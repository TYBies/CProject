using AutoMapper;
using FootballMatches.API.DTOs;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using System.Xml.Linq;

namespace FootballMatches.API.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        private readonly IMapper<Match, MatchDto> _mapper;

        public MatchService(IMatchRepository matchRepository, IMapper<Match, MatchDto> mapper)
        {
            _matchRepository = matchRepository;           
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByMatchDayAsync(int matchDay)
        {
            var matches = await _matchRepository.GetMatchesByMatchDayAsync(matchDay);
          
            return _mapper.MapToDtos(matches);
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByDateRangeAsync(DateTime start, DateTime end)
        {
            var matches = await _matchRepository.GetMatchesByDateRangeAsync(start, end);
            return _mapper.MapToDtos(matches);
        }

        public async Task<IEnumerable<int>> GetAvailableMatchDaysAsync()
        {
            return await _matchRepository.GetAvailableMatchDaysAsync();
        }

        public Task<IEnumerable<MatchDto>> GetMatchesByMatchDaySortedByKickoffTimeDescendingAsync(int matchDay)
        {
            throw new NotImplementedException();
        }
    }
}