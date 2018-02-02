using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Taobao.Area.Api.Configurations;
using Taobao.Area.Api.Exceptions;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Jobs
{
    public class DownloadStreetDataJob
    {
        private readonly TaobaoAreaSettings _settings;
        private readonly IHostingEnvironment _env;
        private readonly ILogger<DownloadStreetDataJob> _logger;

        public DownloadStreetDataJob(
            IHostingEnvironment env,
            IOptions<TaobaoAreaSettings> settings,
            ILogger<DownloadStreetDataJob> logger)
        {
            _env = env;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task DownloadAsync(string provinceCode, string cityCode, string districtCode, bool isForce)
        {
            var jsonName = $"{districtCode}.json";
            var jsonPath = Path.Combine(_env.WebRootPath, _settings.JsDirectoryName, jsonName);
            if(File.Exists(jsonPath) && !isForce)
                return;

            var client = new HttpClient();
            var url = string.Format(_settings.TaobaoStreetUrl, provinceCode, cityCode, districtCode);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"下载 {_settings.TaobaoStreetUrl} 失败。StatusCode：{response.StatusCode}");
                throw new TaobaoAreaDomainException($"下载 {_settings.TaobaoStreetUrl} 失败。StatusCode：{response.StatusCode}");//抛异常 让Hangfire 重试
            }
            var context = await response.Content.ReadAsStringAsync();

            var data = Analysis(context);
            await CreatJson(districtCode, data);
        }

        private string Analysis(string str)
        {
            //json不支持引号,去除拼音等
            var temp = str.Ltrim("success:true,result:").Rtrim("});");
            temp = Regex.Replace(temp, "[a-zA-Z]+", "").Replace(" ", "").Replace(",''", "")
                .Replace("'", "\"");
            return temp;
        }

        private async Task CreatJson(string districtCode, string json)
        {
            var jsonName = $"{districtCode}.json";
            var jsonPath = Path.Combine(_env.WebRootPath, _settings.JsDirectoryName, jsonName);
            await File.WriteAllTextAsync(jsonPath, json);
        }
    }
}
