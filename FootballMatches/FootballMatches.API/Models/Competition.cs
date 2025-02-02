namespace FootballMatches.API.Models
{
    public class Competition
    {
        public required string CompetitionId { get; set; }
        public required string CompetitionName { get; set; }
        public required string CompetitionType { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        

        // Navigation properties
        public  Season? Season { get; set; }
        public ICollection<Match>? Matches { get; set; }
    }
}