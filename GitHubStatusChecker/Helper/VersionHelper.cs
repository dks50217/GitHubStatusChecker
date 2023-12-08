using GitHubStatusChecker.Model;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GitHubStatusChecker.Helper
{
    /// <summary>
    /// 提供版本相關的輔助方法。
    /// </summary>
    public class VersionHelper
    {
        /// <summary>
        /// 從指定的配置檔案中讀取最後檢查的發行資訊。
        /// </summary>
        /// <param name="ConfigFilePath">配置檔案的路徑。</param>
        /// <returns>最後檢查的發行資訊，如果檔案不存在則返回 null。</returns>
        public static ReleaseInfo ReadLastCheckedRelease(string ConfigFilePath)
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                return JsonConvert.DeserializeObject<ReleaseInfo>(json);
            }

            return null;
        }

        /// <summary>
        /// 將最後檢查的發行資訊儲存到指定的配置檔案中。
        /// </summary>
        /// <param name="release">要儲存的發行資訊。</param>
        /// <param name="configFilePath">配置檔案的路徑。</param>
        public static void SaveLastCheckedRelease(Release release, string configFilePath)
        {
            var releaseInfo = new ReleaseInfo
            {
                TagName = release.TagName,
                PublishedAt = release.PublishedAt
            };

            var json = JsonConvert.SerializeObject(releaseInfo, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(configFilePath, json);
        }
    }
}
