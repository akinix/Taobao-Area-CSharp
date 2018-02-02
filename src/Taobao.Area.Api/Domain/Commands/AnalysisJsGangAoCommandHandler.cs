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
    public class AnalysisJsGangAoCommandHandler : IRequestHandler<AnalysisJsGangAoCommand, bool>
    {
        private readonly AreaContextService _areaContextService;

        public AnalysisJsGangAoCommandHandler(AreaContextService areaContextService)
        {
            _areaContextService = areaContextService;
        }

        public async Task<bool> Handle(AnalysisJsGangAoCommand command, CancellationToken cancellationToken)
        {
            //将澳追加到A-G
            var array = JsonConvert.DeserializeObject<JArray>(_areaContextService.GangAoString);
            var aoCode = "820000";
            var ao = array.First(i => i[0].Value<string>() == aoCode);
            var pao = new List<object> { aoCode.ToInt(), ao[1][0].Value<string>(), AreaContextService.FirstItemKey.ToInt() };
            (_areaContextService.GetFirstDicItem()[AreaContextService.KeyAg] as List<object>)?.Add(pao);
            RecursiveAdd(array, aoCode);

            //将香港追加到T-Z
            var gangCode = "810000";
            var gang = array.First(i => i[0].Value<string>() == gangCode);
            var pgang = new List<object> { gangCode.ToInt(), gang[1][0].Value<string>(), AreaContextService.FirstItemKey.ToInt() };
            (_areaContextService.GetFirstDicItem()[AreaContextService.KeyTz] as List<object>)?.Add(pgang);
            RecursiveAdd(array, gangCode);
            return true;
        }

        private void RecursiveAdd(JArray ja, string pid)
        {
            var subs = ja.Where(i => i[2].Value<string>() == pid).ToList();
            if (subs.Count == 0)
                return;
            var rows = new List<object>();
            foreach (var sub in subs)
            {
                var subid = sub[0].Value<string>();
                var props = new List<object> { subid.ToInt(), sub[1][0].Value<string>(), pid.ToInt() };
                rows.Add(props);
                RecursiveAdd(ja, subid);
            }
            _areaContextService.MainDictionary.TryAdd(pid, rows);
        }
    }
}