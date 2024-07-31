using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface ICompetitionRepository
    {
        Task AddCompetitionsAsync(IEnumerable<Competition> competitions);
    }
}