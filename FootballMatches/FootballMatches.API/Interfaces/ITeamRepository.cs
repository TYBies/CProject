using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface ITeamRepository
    {
        Task AddTeamsAsync(IEnumerable<Team> teams);
    }
}