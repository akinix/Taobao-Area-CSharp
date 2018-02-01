using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Taobao.Area.Api.Configurations;
using Taobao.Area.Api.Domain.Events;
using Taobao.Area.Api.Domain.Services;

namespace Taobao.Area.Api.Domain.Commands
{
    public class CreateDataJsCommandHandler : IRequestHandler<CreateDataJsCommand>
    {
        private readonly IMediator _mediator;
        private readonly AreaContextService _areaContextService;
        private readonly IHostingEnvironment _env;
        private readonly TaobaoAreaSettings _settings;

        private static object _lockObj = new object();

        public CreateDataJsCommandHandler(
            IMediator mediator,
            AreaContextService areaContextService,
            IHostingEnvironment env,
            IOptions<TaobaoAreaSettings> settings
            )
        {
            _mediator = mediator;
            _areaContextService = areaContextService;
            _env = env;
            _settings = settings.Value;
        }

        public async Task Handle(CreateDataJsCommand command, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(_areaContextService.MainDictionary);
            var jscontent = FormatJs(json);
            var jsname = CreatJs(jscontent);
            await _mediator.Publish(new CreateDataJsCompletedEvent(jsname), cancellationToken);
        }

        private string FormatJs(string json)
        {
            return string.Format(_settings.JsTemplate, json);
        }

        private string CreatJs(string js)
        {
            var jsName = string.Format(_settings.AreaPickerDataJsName, _settings.TaobaoJsVersion);
            var jsPath = Path.Combine(_env.WebRootPath, _settings.JsDirectoryName, jsName);
            lock (_lockObj)
            {
                File.WriteAllTextAsync(jsPath, js);
            }
            return jsPath;
        }
    }
}
