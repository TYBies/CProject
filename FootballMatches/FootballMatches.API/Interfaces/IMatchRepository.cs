using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> GetMatchesByMatchDayAsync(int matchDay);
        Task<IEnumerable<Match>> GetMatchesByDateRangeAsync(DateTime start, DateTime end);
        Task AddMatchesAsync(IEnumerable<Match> matches);
        Task<IEnumerable<int>> GetAvailableMatchDaysAsync();
        Task<Match> GetByIdAsync(string id);
        Task<IEnumerable<Match>> GetAllAsync();
        Task AddAsync(Match match);        
        Task UpdateAsync(Match match);
        Task DeleteAsync(string id);
    }
}