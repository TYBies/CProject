using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface IStadiumRepository
    {
        Task AddStadiumsAsync(IEnumerable<Stadium> stadiums);
    }
}