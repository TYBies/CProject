using System.Xml.Linq;
using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface IXmlParserService
    {
        IEnumerable<Match> ExtractMatches(XDocument xDocument);
        IEnumerable<Team> ExtractTeams(XDocument xDocument);
        IEnumerable<Competition> ExtractCompetitions(XDocument xDocument);
        IEnumerable<Season> ExtractSeasons(XDocument xDocument);
        IEnumerable<Stadium> ExtractStadiums(XDocument xDocument);

    }
}