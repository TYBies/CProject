using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly ApplicationDbContext _context;
        public SeasonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSeasonsAsync(IEnumerable<Season> seasons)
        {
            var competitionIds = await _context.Competitions.Select(c => c.CompetitionId).ToListAsync();
            var validSeasons = seasons.Where(s => competitionIds.Contains(s.CompetitionId)).ToList();

            await _context.Seasons.AddRangeAsync(seasons);
            await _context.SaveChangesAsync();
        }
    }
}
