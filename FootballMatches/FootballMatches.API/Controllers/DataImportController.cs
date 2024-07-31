using FootballMatches.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatches.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataImportController : ControllerBase
    {
        private readonly IDataImportService _dataImportService;
        private readonly ILogger<DataImportController> _logger;

        public DataImportController(IDataImportService dataImportService, ILogger<DataImportController> logger)
        {
            _dataImportService = dataImportService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (file.ContentType != "text/xml" && file.ContentType != "application/xml")
            {
                return BadRequest("File must be an XML document.");
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var xmlContent = await reader.ReadToEndAsync();

                await _dataImportService.ImportDataFromXmlAsync(xmlContent);

                return Ok("XML data imported successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing the XML file.");
                return StatusCode(500, "An error occurred while processing the file. Please try again.");
            }
        }
    }
}