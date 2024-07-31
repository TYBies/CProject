using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface ISeasonRepository
    {
        Task AddSeasonsAsync(IEnumerable<Season> seasons);
    }
}