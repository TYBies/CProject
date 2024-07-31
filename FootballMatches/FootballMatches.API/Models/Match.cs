using System;
using System.ComponentModel.DataAnnotations;
namespace FootballMatches.API.Models
{
    public class Match
    {
        [Key]
        public required string MatchId { get; set; }
        public int MatchDay { get; set; }
        public required string MatchType { get; set; }
        public DateTime PlannedKickoffTime { get; set; }
        public bool NeutralVenue { get; set; }
        public bool MatchDateFixed { get; set; }
        public required int DLProviderId { get; set; }
        

        // Foreign keys
        public required string CompetitionId { get; set; }
        public required string StadiumId { get; set; }
        public required string HomeTeamId { get; set; }
        public required string AwayTeamId { get; set; }

        // Navigation properties
        public  Competition? Competition { get; set; }      
        public  Stadium? Stadium { get; set; }
        public  Team? HomeTeam { get; set; }
        public  Team? AwayTeam { get; set; }
    }
}