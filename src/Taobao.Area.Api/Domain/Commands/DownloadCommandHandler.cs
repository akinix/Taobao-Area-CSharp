using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Taobao.Area.Api.Configurations;
using Taobao.Area.Api.Events;

namespace Taobao.Area.Api.Domain.Commands
{
    public class DownloadCommandHandler : IRequestHandler<DownloadCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment _env;
        private readonly TaobaoAreaSettings _settings;
        private readonly ILogger<DownloadCommandHandler> _logger;

        private const string TempDirectoryName = "temp";

        public DownloadCommandHandler(
            IMediator mediator,
            IHostingEnvironment env, 
            IOptions<TaobaoAreaSettings> settings, 
            ILogger<DownloadCommandHandler> logger)
        {
            _mediator = mediator;
            _env = env;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<string> Handle(DownloadCommand command, CancellationToken cancellationToken)
        {
            // 非强制下载 先检查temp文件夹下有无缓存文件
            var tempJs = CheckTempJs();
            if(command.IsForce && string.IsNullOrEmpty(tempJs))
            {
                tempJs = await Download();
            }
            var e = new DownloadedEvent(tempJs);
            await _mediator.Publish(e);
            return tempJs;
        }

        private string GetTempDirectory()
        {
            var tempDir = Path.Combine(_env.ContentRootPath, TempDirectoryName);
            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);
            return tempDir;
        }

        // 检查临时文件
        private string CheckTempJs()
        {
            var fileName = Path.GetFileName(_settings.TaobaoAreaJsUrl);
            var fullName = Path.Combine(GetTempDirectory(), fileName);
            if (File.Exists(fullName))
                return fullName;
            else
                return string.Empty;
        }

        // 下载
        private async Task<string> Download()
        {
            var _client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _settings.TaobaoAreaJsUrl);
            var response = await _client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"下载 {_settings.TaobaoAreaJsUrl} 失败。");
                return string.Empty;
            }
            var context = await response.Content.ReadAsStringAsync();
            var fileName = Path.GetFileName(_settings.TaobaoAreaJsUrl);
            var fullName = Path.Combine(GetTempDirectory(), fileName);
            await File.WriteAllTextAsync(fullName, context);
            _logger.LogInformation($"下载 {_settings.TaobaoAreaJsUrl} 成功。临时存放路径：{fullName}");
            return fullName;
        }
    }
}
