namespace FootballMatches.API.Interfaces
{
    public interface IDataImportService
    {
        Task ImportDataFromXmlAsync(string xmlContent);
    }
}
