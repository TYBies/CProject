namespace FootballMatches.API.Models
{
    public class Season
    {
        public required string SeasonId { get; set; }
        public required string SeasonName { get; set; }

        // Foreign key for Competition
        public required string CompetitionId { get; set; }

        // Navigation property
        public Competition? Competition { get; set; }
    }
}