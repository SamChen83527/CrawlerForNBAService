using CrawlerForNBAService.Services.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlerForNBAService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : Controller
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayersService _playersService;
        public PlayersController(ILogger<PlayersController> logger, IPlayersService playerService)
        {
            _logger = logger;
            _playersService = playerService;
        }

        [HttpGet("{alphabet}")]
        public async Task<IActionResult> GetPlayersCareerStatisticsCSVAsync(string alphabet)
        {
            try
            {
                var players = await _playersService.GetPlayersByAlphabetAsync(alphabet);
                players = players.OrderBy(p => p.Name).ToList();

                await using var memoryStream = new MemoryStream();
                await using var streamWriter = new StreamWriter(memoryStream);
                await using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                await csvWriter.WriteRecordsAsync(players);
                await streamWriter.FlushAsync();

                var result = new FileContentResult(memoryStream.ToArray(), "application/octet-stream")
                {
                    FileDownloadName = $"{alphabet.ToUpper()}.csv"
                };
                return result;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(500);
            }
        }
    }
}
