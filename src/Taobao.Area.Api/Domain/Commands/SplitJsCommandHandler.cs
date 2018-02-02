using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Taobao.Area.Api.Domain.Events;
using Taobao.Area.Api.Domain.Services;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Commands
{
    /// <summary>
    /// 此方法只用来分解淘宝Js
    /// </summary>
    public class SplitJsCommandHandler : IRequestHandler<SplitJsCommand>
    {
        private readonly IMediator _mediator;
        private readonly AreaContextService _areaContextService;

        public SplitJsCommandHandler(
            IMediator mediator,
            IHostingEnvironment env,
            AreaContextService areaContextService,
            ILogger<SplitJsCommandHandler> logger)
        {
            _mediator = mediator;
            _areaContextService = areaContextService;
        }

        public async Task Handle(SplitJsCommand command, CancellationToken cancellationToken)
        {
            var content = await File.ReadAllTextAsync(command.TempJsName, cancellationToken);

            // 获取除香港澳门台湾的省市区
            var matches = content.TrimEnd(';').Split("\"A-G\"");
            matches = matches[1].Split(";return t=e}(),r=function(t){var e=");
            var strProvince = "{\"A-G\"" + matches[0];
            var strArear = matches[1].Rtrim("return t=e}(),c=function(t){var e=").Rtrim(";");

            // 获取港澳台马其他
            matches = matches[1].Split("return t=e}(),h=function(e)");
            Regex reg = new Regex(@",[a-zA-Z]=function");
            var other = reg.Replace(matches[0].Ltrim("return t=e}(),c=function(t){var e="), ",o=function").Split(";return t=e}(),o=function(t){var e="); // [0]港澳 [1]台湾 [2]马来西亚 [3]欧美

            var strGangAo = other[0];
            var strTaiwan = other[1];

            _areaContextService.SetProvinceString(strProvince);
            _areaContextService.SetAreaString(strArear);
            _areaContextService.SetGangAoString(strGangAo);
            _areaContextService.SetTaiwanString(strTaiwan);

            // 触发拆分js完成事件
            await _mediator.Publish(new SplitJsCompletedEvent(), cancellationToken);
        }
    }
}
