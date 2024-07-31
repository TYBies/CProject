using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly ApplicationDbContext _context;

        public StadiumRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStadiumsAsync(IEnumerable<Stadium> stadiums)
        {
            var existingStadiumIds = new HashSet<string>(await _context.Stadiums.Select(s => s.StadiumId).ToListAsync());
            var newStadiums = stadiums.Where(s => !existingStadiumIds.Contains(s.StadiumId)).ToList();

            await _context.Stadiums.AddRangeAsync(newStadiums);
            await _context.SaveChangesAsync();
        }
    }
}
