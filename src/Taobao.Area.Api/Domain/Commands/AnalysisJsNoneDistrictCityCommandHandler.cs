using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taobao.Area.Api.Domain.Services;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisJsNoneDistrictCityCommandHandler : IRequestHandler<AnalysisJsNoneDistrictCityCommand, bool>
    {
        private const string NoneDistrictCityItemKey = "NoneDistrictCity";

        private readonly AreaContextService _areaContextService;

        public AnalysisJsNoneDistrictCityCommandHandler(AreaContextService areaContextService)
        {
            _areaContextService = areaContextService;
        }

        public async Task<bool> Handle(AnalysisJsNoneDistrictCityCommand command, CancellationToken cancellationToken)
        {
            // 将澳追加到A-G
            var array = JsonConvert.DeserializeObject<JArray>(_areaContextService.NoneDistrictCityString);

            _areaContextService.MainDictionary.TryAdd(NoneDistrictCityItemKey, array);

            return true;
        }
    }
}