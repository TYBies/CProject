using System.Xml.Linq;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;

namespace FootballMatches.API.Services
{
    public class XmlParserService : IXmlParserService
    {
        private readonly ILogger<XmlParserService> _logger;

        public XmlParserService(ILogger<XmlParserService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Match> ExtractMatches(XDocument xDocument)
        {
            try
            {
                return xDocument.Descendants("Fixture")
                    .Select(e => new Match
                    {
                        MatchId = e.Attribute("MatchId")?.Value ?? throw new ArgumentNullException("MatchId is required"),
                        MatchDay = int.Parse(e.Attribute("MatchDay")?.Value ?? "0"),
                        MatchType = e.Attribute("MatchType")?.Value ?? throw new ArgumentNullException("MatchType is required"),
                        PlannedKickoffTime = DateTime.Parse(e.Attribute("PlannedKickoffTime")?.Value ?? DateTime.MinValue.ToString()),
                        NeutralVenue = bool.Parse(e.Attribute("NeutralVenue")?.Value ?? "false"),
                        MatchDateFixed = bool.Parse(e.Attribute("MatchDateFixed")?.Value ?? "false"),
                        DLProviderId = int.Parse(e.Attribute("DlProviderId")?.Value ?? throw new ArgumentNullException("DLProviderId is required")),
                        CompetitionId = e.Attribute("CompetitionId")?.Value ?? throw new ArgumentNullException("CompetitionId is required"),
                        StadiumId = e.Attribute("StadiumId")?.Value ?? throw new ArgumentNullException("StadiumId is required"),
                        HomeTeamId = e.Attribute("HomeTeamId")?.Value ?? throw new ArgumentNullException("HomeTeamId is required"),
                        AwayTeamId = e.Attribute("GuestTeamId")?.Value ?? throw new ArgumentNullException("AwayTeamId is required")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting matches from XML");
                throw;
            }
        }

        public IEnumerable<Team> ExtractTeams(XDocument xDocument)
        {
            try
            {
                var homeTeams = xDocument.Descendants("Fixture")
                    .Select(e => new Team
                    {
                        TeamId = e.Attribute("HomeTeamId")?.Value ?? throw new ArgumentNullException("HomeTeamId is required"),
                        TeamName = e.Attribute("HomeTeamName")?.Value ?? throw new ArgumentNullException("HomeTeamName is required")
                    });

                var awayTeams = xDocument.Descendants("Fixture")
                    .Select(e => new Team
                    {
                        TeamId = e.Attribute("GuestTeamId")?.Value ?? throw new ArgumentNullException("GuestTeamId is required"),
                        TeamName = e.Attribute("GuestTeamName")?.Value ?? throw new ArgumentNullException("GuestTeamName is required")
                    });

                return homeTeams.Concat(awayTeams)
                    .GroupBy(t => t.TeamId)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting teams from XML");
                throw;
            }
        }

        public IEnumerable<Competition> ExtractCompetitions(XDocument xDocument)
        {
            try
            {
                return xDocument.Descendants("Fixture")
                    .Select(e => new Competition
                    {
                        CompetitionId = e.Attribute("CompetitionId")?.Value ?? throw new ArgumentNullException("CompetitionId is required"),
                        CompetitionName = e.Attribute("CompetitionName")?.Value ?? throw new ArgumentNullException("CompetitionName is required"),
                        CompetitionType = e.Attribute("CompetitionType")?.Value ?? throw new ArgumentNullException("CompetitionType is required"),
                        StartDate = DateTime.Parse(e.Attribute("StartDate")?.Value ?? throw new ArgumentNullException("StartDate is required")),
                        EndDate = DateTime.Parse(e.Attribute("EndDate")?.Value ?? throw new ArgumentNullException("EndDate is required"))
                    })
                    .GroupBy(c => c.CompetitionId)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting competitions from XML");
                throw;
            }
        }

        public IEnumerable<Season> ExtractSeasons(XDocument xDocument)
        {
            try
            {
                return xDocument.Descendants("Fixture")
                    .Select(e => new Season
                    {
                        SeasonId = e.Attribute("SeasonId")?.Value ?? throw new ArgumentNullException("SeasonId is required"),
                        SeasonName = e.Attribute("Season")?.Value ?? throw new ArgumentNullException("Season is required"),
                        CompetitionId = e.Attribute("CompetitionId")?.Value ?? throw new ArgumentNullException("CompetitionId is required")
                    })
                    .GroupBy(s => s.SeasonId)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting seasons from XML");
                throw;
            }
        }

        public IEnumerable<Stadium> ExtractStadiums(XDocument xDocument)
        {
            try
            {
                return xDocument.Descendants("Fixture")
                    .Select(e => new Stadium
                    {
                        StadiumId = e.Attribute("StadiumId")?.Value ?? throw new ArgumentNullException("StadiumId is required"),
                        StadiumName = e.Attribute("StadiumName")?.Value ?? throw new ArgumentNullException("StadiumName is required")
                    })
                    .GroupBy(s => s.StadiumId)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting stadiums from XML");
                throw;
            }
        }
    }
}