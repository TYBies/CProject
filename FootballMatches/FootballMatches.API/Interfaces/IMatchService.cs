using FootballMatches.API.DTOs;


namespace FootballMatches.API.Interfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchDto>> GetMatchesByMatchDayAsync(int matchDay);
        Task<IEnumerable<MatchDto>> GetMatchesByDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<int>> GetAvailableMatchDaysAsync();
        Task<IEnumerable<MatchDto>> GetMatchesByMatchDaySortedByKickoffTimeDescendingAsync(int matchDay);
    }
}