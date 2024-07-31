using FootballMatches.API.Interfaces;
using FootballMatches.API.Utility;

namespace FootballMatches.API.Services
{
    public class DataImportService : IDataImportService
    {
        private readonly IXmlParserService _xmlParserService;
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IStadiumRepository _stadiumRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly ILogger <DataImportService> _logger;
      

        public DataImportService(
            IXmlParserService xmlParserService,
            IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            ICompetitionRepository competitionRepository,
            IStadiumRepository stadiumRepository,
            ISeasonRepository seasonRepository,
            ILogger<DataImportService> logger
            )
        {
            _xmlParserService = xmlParserService;
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _competitionRepository = competitionRepository;
            _stadiumRepository = stadiumRepository;
            _seasonRepository = seasonRepository;
            _logger = logger;
        }
        public async Task ImportDataFromXmlAsync(string xmlContent)
        {
            try
            {
                var xDocument = XmlUtility.ParseXml(xmlContent);

                var competitions = _xmlParserService.ExtractCompetitions(xDocument);
                await _competitionRepository.AddCompetitionsAsync(competitions);

                var seasons = _xmlParserService.ExtractSeasons(xDocument);
                await _seasonRepository.AddSeasonsAsync(seasons);                

                var teams = _xmlParserService.ExtractTeams(xDocument);
                await _teamRepository.AddTeamsAsync(teams);

                var stadiums = _xmlParserService.ExtractStadiums(xDocument);
                await _stadiumRepository.AddStadiumsAsync(stadiums);

                var matches = _xmlParserService.ExtractMatches(xDocument);
                await _matchRepository.AddMatchesAsync(matches);

                _logger.LogInformation("Data import completed successfully");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while importing data");
                throw;
            }
            
        }
    }

    
}
