using System.Collections.Generic;

namespace FootballMatches.API.Models
{
    public class Stadium
    {
        public required string StadiumId { get; set; }
        public required string StadiumName { get; set; }

        // Navigation property
        public  ICollection<Match>? Matches { get; set; }
    }
}