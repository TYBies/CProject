using System.Xml.Linq;

namespace FootballMatches.API.Utility
{
    public class XmlUtility
    {
        public static XDocument ParseXml(string xmlContent)
        {
            try
            {
                return XDocument.Parse(xmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong:");
                Console.WriteLine(ex.Message);
            }
            return new XDocument();
        }
    }
    
}
