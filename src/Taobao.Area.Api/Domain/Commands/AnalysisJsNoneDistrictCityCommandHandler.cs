using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taobao.Area.Api.Domain.Events;
using Taobao.Area.Api.Domain.Services;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisJsNoneDistrictCityCommandHandler : IRequestHandler<AnalysisJsNoneDistrictCityCommand, bool>
    {
        private const string NoneDistrictCityItemKey = "NoneDistrictCity";

        private readonly IMediator _mediator;
        private readonly AreaContextService _areaContextService;

        public AnalysisJsNoneDistrictCityCommandHandler(
            IMediator mediator, 
            AreaContextService areaContextService)
        {
            _mediator = mediator;
            _areaContextService = areaContextService;
        }

        public async Task<bool> Handle(AnalysisJsNoneDistrictCityCommand command, CancellationToken cancellationToken)
        {
            // 处理部分城市无区县但是有街道的数据
            var array = JsonConvert.DeserializeObject<JArray>(_areaContextService.NoneDistrictCityString);
            _areaContextService.MainDictionary.TryAdd(NoneDistrictCityItemKey, array);

            foreach (var item in array)
            {
                var pId = item.Value<string>();
                var ppId = pId.Substring(0, 4) + "00";
                await _mediator.Publish(new NoneDistrictCityAddedEvent(ppId, pId), cancellationToken);
            }
            return true;
        }
    }
}