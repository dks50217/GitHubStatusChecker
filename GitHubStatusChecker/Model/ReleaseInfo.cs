using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubStatusChecker.Model
{
    public class ReleaseInfo
    {
        public string TagName { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
    }
}
