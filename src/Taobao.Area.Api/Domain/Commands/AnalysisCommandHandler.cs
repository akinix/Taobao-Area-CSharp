using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Taobao.Area.Api.Configurations;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisCommandHandler : IRequestHandler<AnalysisCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment _env;
        private readonly TaobaoAreaSettings _settings;
        private readonly ILogger<DownloadCommandHandler> _logger;

        private const string TempDirectoryName = "temp";

        public AnalysisCommandHandler(
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

        public async Task<string> Handle(AnalysisCommand command, CancellationToken cancellationToken)
        {
            await Analysis(command.TempJsName);
            return string.Empty;
        }

        private async Task Analysis(string file)
        {
            var str = await File.ReadAllTextAsync(file);

            // 获取除香港澳门台湾的省市区
            var matches = str.TrimEnd(';').Split("\"A-G\"");

            matches = matches[1].Split(";return t=e}(),r=function(t){var e=");

            var province = "{\"A-G\"" + matches[0];

            matches = matches[1].Split("return t=e}(),h=function(e)");

            //$str = preg_replace("/,[a-zA-Z]=function/", ",o=function", ltrim($matches[0], "return t=e}(),o=function(t){var e="));

            var temp1 = matches[0].Ltrim("return t=e}(),o=function(t){var e=");

            Regex reg = new Regex(@",[a-zA-Z]=function");
            //str = reg.Replace(str, "display=localhost");

            var temp2 = reg.Replace(temp1, ",o=function");

            var other = temp2.Split(";return t=e}(),o=function(t){var e="); // other[0] 台湾 other[1] 马来西亚 other[2] 欧美
            


        }

    }
}
