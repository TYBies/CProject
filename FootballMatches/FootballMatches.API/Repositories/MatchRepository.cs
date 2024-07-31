using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Match = FootballMatches.API.Models.Match;


namespace FootballMatches.API.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;

        public MatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
        }

        public async Task AddMatchesAsync(IEnumerable<Match> matches)
        {
            var existingMatchIds = new HashSet<string>(await _context.Matches.Select(m => m.MatchId).ToListAsync());
            var competitionIds = new HashSet<string>(await _context.Competitions.Select(c => c.CompetitionId).ToListAsync());
            var teamIds = new HashSet<string>(await _context.Teams.Select(t => t.TeamId).ToListAsync());
            var stadiumIds = new HashSet<string>(await _context.Stadiums.Select(s => s.StadiumId).ToListAsync());

            var validMatches = matches.Where(m =>
                !existingMatchIds.Contains(m.MatchId) &&
                competitionIds.Contains(m.CompetitionId) &&
                teamIds.Contains(m.HomeTeamId) &&
                teamIds.Contains(m.AwayTeamId) &&
                stadiumIds.Contains(m.StadiumId)
            ).ToList();

            await _context.Matches.AddRangeAsync(validMatches);
            await _context.SaveChangesAsync();
        }        

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(m => m.Competition)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .ToListAsync();
        }       

        public async Task<IEnumerable<Match>> GetMatchesByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Matches
                 .Where(m => m.PlannedKickoffTime >= start && m.PlannedKickoffTime <= end)
                 .Include(m => m.Competition)
                 .Include(m => m.HomeTeam)
                 .Include(m => m.AwayTeam)
                 .Include(m => m.Stadium)
                 .OrderBy(m => m.PlannedKickoffTime)
                 .ToListAsync();
        }           

        public async Task<IEnumerable<Match>> GetMatchesByMatchDayAsync(int matchDay)
        {
            return await _context.Matches
                .Where(m => m.MatchDay == matchDay)
                .Include(m => m.Competition)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .OrderBy(m => m.PlannedKickoffTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAvailableMatchDaysAsync()
        {            
                return await _context.Matches
                    .Select(m => m.MatchDay)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToListAsync();           
        }

        public Task<Match> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Match match)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }        
}