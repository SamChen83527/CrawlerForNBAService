using AngleSharp;
using AngleSharp.Dom;
using CrawlerForNBAService.Models;
using CrawlerForNBAService.Services.Interfaces;
using CrawlerForNBAService.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrawlerForNBAService.Services
{
    public class PlayersService : IPlayersService
    {
        /// <summary>
        /// 依據字母取得球員名、球員頁面資源檔名(e.g. /players/a/abdelal01.html)
        /// </summary>
        /// <param name="alphabet"> 字母 </param>
        /// <returns> 該字母類別內所有球員清單 </returns>
        public async Task<List<Player>> GetPlayersByAlphabetAsync(string alphabet)
        {
            var playerList = new List<Player>();
            var document = await CrawlerSettings.context.OpenAsync($"{CrawlerSettings.targetUrl}/players/{alphabet.ToLower()}");
            // 依據<th>取出球員  
            var players = document
                .QuerySelector("#players > tbody")
                .GetElementsByTagName("th");

            // References
            // https://csharpkh.blogspot.com/2019/09/CSharp-Task-Run-StartNew-Thread-Wait-Cancellation-CancellationToken-Exception.html
            // https://docs.microsoft.com/zh-tw/dotnet/standard/parallel-programming/task-based-asynchronous-programming
            var taskArray = new List<Task>();
            foreach (var playerEle in players)
            {
                var playerName = playerEle.GetElementsByTagName("a")[0].Text();
                var playerResouceName = playerEle.GetElementsByTagName("a")[0].GetAttribute("href");
                var player = new Player(playerName, playerResouceName);

                // 多執行緒取得資料
                taskArray.Add(Task.Run(async () => {
                    await player.SetPlayerCareerStatistics();
                    playerList.Add(player);
                }));
            }
            Task.WhenAll(taskArray).Wait();

            return playerList;
        }
    }
}
