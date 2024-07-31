using System.ComponentModel.DataAnnotations;

namespace FootballMatches.API.Models
{
    public class Team
    {
        [Key]
        public required string TeamId { get; set; }
        public required string TeamName { get; set; }

        // Navigation properties
        public  ICollection<Match>? HomeMatches { get; set; }
        public  ICollection<Match>? AwayMatches { get; set; }
    }
}