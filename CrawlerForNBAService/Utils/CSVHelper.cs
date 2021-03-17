using CrawlerForNBAService.Models;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CrawlerForNBAService.Utils
{
    public class CSVHelper
    {        
        /// <summary>
        /// 將球員資料寫入csv檔
        /// </summary>
        /// <param name="alphabet"> 字母 </param>
        /// <param name="playerStatisticsList"> 球員生涯統計資料 </param>
        public static void WriteCsv(string alphabet, List<Player> playerStatisticsList)
        {
            Directory.CreateDirectory("Data");

            using var writer = new StreamWriter($"Data/{alphabet.ToUpper()}.csv");

            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(playerStatisticsList);
        }
    }
}
