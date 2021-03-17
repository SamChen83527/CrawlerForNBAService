using CrawlerForNBAService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlerForNBAService.Services.Interfaces
{
    public interface IPlayersService : IService
    {
        Task<List<Player>> GetPlayersByAlphabetAsync(string alphabet);
    }
}
