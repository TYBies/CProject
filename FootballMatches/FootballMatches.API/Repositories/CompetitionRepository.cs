using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly ApplicationDbContext _context;

        public CompetitionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCompetitionsAsync(IEnumerable<Competition> competitions)
        {
            var existingCompetitionIds = new HashSet<string>(await _context.Competitions.Select(c => c.CompetitionId).ToListAsync());
            var newCompetitions = competitions.Where(c => !existingCompetitionIds.Contains(c.CompetitionId)).ToList();

            await _context.Competitions.AddRangeAsync(newCompetitions);
            await _context.SaveChangesAsync();
        }
    }
}
