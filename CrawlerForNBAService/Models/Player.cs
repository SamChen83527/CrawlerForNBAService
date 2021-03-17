using AngleSharp;
using AngleSharp.Dom;
using CrawlerForNBAService.Utils;
using CsvHelper.Configuration.Attributes;
using System;
using System.Threading.Tasks;

namespace CrawlerForNBAService.Models
{
    public class Player
    {
        public Player(string name, string playerResouceName)
        {
            Name = name;
            PlayerResouceName = playerResouceName;
        }

        [Name("Player")]
        public string Name { get; set; }

        [Ignore]
        public string PlayerResouceName { get; set; }

        public int? G { get; set; }

        public float? PTS { get; set; }

        public float? TRB { get; set; }

        public float? AST { get; set; }

        [Name("FG(%)")]
        public float? FGP { get; set; }

        [Name("FG3(%)")]
        public float? FG3P { get; set; }

        [Name("FT(%)")]
        public float? FTP { get; set; }

        [Name("eFG(%)")]
        public float? eFGP { get; set; }

        public float? PER { get; set; }

        public float? WS { get; set; }

        /// <summary>
        /// 取得該球員生涯統計
        /// </summary>
        /// <param name="player"> 球員 </param>
        /// <returns></returns>
        public async Task SetPlayerCareerStatistics()
        {
            var document = await CrawlerSettings.context.OpenAsync($"{CrawlerSettings.targetUrl}{PlayerResouceName}");

            // 取得球員生涯表單
            #region example
            /*
                < div class="stats_pullout">
                    <div>
                    </div>
                    <div class="p1">
                        <div>
                            <h4 class="poptip" data-tip="Games">G</h4>
                            <p></p>
                            <p>256</p>
                        </div>

                        <div>
                            <h4 class="poptip" data-tip="Points">PTS</h4>
                            <p></p>
                            <p>5.7</p>
                        </div>

                        <div>
                        </div>

                        <div>
                        </div>
                    </div>

                    <div class="p2">
                    </div>

                    <div class="p3">
                    </div>
                </div>
            */
            #endregion
            var statisticsFromCrawler = document
                .GetElementsByClassName("stats_pullout");

            // 解析表單
            ParsePlayerStatistics(statisticsFromCrawler[0], this);

            Console.WriteLine($"{Name} {G} {PTS} {TRB} {AST} {FGP} {FG3P} {FTP} {eFGP} {PER} {WS}");
        }

        /// <summary>
        /// 從 html 解析球員生涯統計資料
        /// </summary>
        /// <param name="statistics"> html原始資料 </param>
        /// <param name="player"> 球員 </param>
        private void ParsePlayerStatistics(IElement statistics, Player player)
        {
            var statisticsParts = statistics.QuerySelectorAll("#info > div.stats_pullout > div.p1,.p2,.p3");

            foreach (var part in statisticsParts)
            {
                var statisticsTypes = part.GetElementsByTagName("div");
                foreach (var item in statisticsTypes)
                {
                    switch (item.GetElementsByTagName("h4")[0].Text())
                    {
                        case "G":
                            if (int.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _G))
                                player.G = _G;
                            break;
                        case "PTS":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _PTS))
                                player.PTS = _PTS;
                            break;
                        case "TRB":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _TRB))
                                player.TRB = _TRB;
                            break;
                        case "AST":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _AST))
                                player.AST = _AST;
                            break;
                        case "FG%":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _FGP))
                                player.FGP = _FGP;
                            break;
                        case "FG3%":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _FG3P))
                                player.FG3P = _FG3P;
                            break;
                        case "FT%":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _FTP))
                                player.FTP = _FTP;
                            break;
                        case "eFG%":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _eFGP))
                                player.eFGP = _eFGP;
                            break;
                        case "PER":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _PER))
                                player.PER = _PER;
                            break;
                        case "WS":
                            if (float.TryParse(item.QuerySelector("p:nth-child(3)").Text(), out var _WS))
                                player.WS = _WS;
                            break;
                    }
                }
            }
        }
    }
}
