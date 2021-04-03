using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_await
{
    public class ProgressReport
    {
        public int PercentageComplete { get; set; } = 0;
        public List<WebsiteDataModel> SitesDownloaded { get; set; } = new List<WebsiteDataModel>();
    }
}
