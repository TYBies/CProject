using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTeamsAsync(IEnumerable<Team> teams)
        {
            var existingTeamIds = new HashSet<string>(await _context.Teams.Select(t => t.TeamId).ToListAsync());
            var newTeams = teams.Where(t => !existingTeamIds.Contains(t.TeamId)).ToList();

            await _context.Teams.AddRangeAsync(newTeams);
            await _context.SaveChangesAsync();
        }
    }
}
