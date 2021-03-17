using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlerForNBAService.Utils
{
    public class CrawlerSettings
    {
        public static readonly string targetUrl = "https://www.basketball-reference.com";
        private static readonly IConfiguration config = Configuration.Default.WithDefaultLoader();
        public static readonly IBrowsingContext context = BrowsingContext.New(config);
    }
}
