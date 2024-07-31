namespace FootballMatches.API.DTOs
{
    public class MatchDto
    {
        public string? MatchId { get; set; }
        public  int DLProviderId {  get; set; }
        public int MatchDay { get; set; }
        public string? CompetitionId { get; set; }
        public DateTime KickoffTime { get; set; }
        public TeamDto? HomeTeamDto { get; set; }
        public TeamDto? AwayTeamDto { get; set; }
        public StadiumDto? StadiumDto { get; set; }        
       
          
    }
}

