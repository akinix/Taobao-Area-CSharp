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
    public class AnalysisJsTaiwanCommandHandler : IRequestHandler<AnalysisJsTaiwanCommand, bool>
    {
        private readonly AreaContextService _areaContextService;

        public AnalysisJsTaiwanCommandHandler(AreaContextService areaContextService)
        {
            _areaContextService = areaContextService;
        }

        public async Task<bool> Handle(AnalysisJsTaiwanCommand command, CancellationToken cancellationToken)
        {
            //将台湾追加到T-Z
            var array = JsonConvert.DeserializeObject<JArray>(_areaContextService.TaiwanString);
            var twCode = "710000";
            var tw = array.First(i => i[0].Value<string>() == twCode);
            var ptw = new List<object> { twCode.ToInt(), tw[1][0].Value<string>(), AreaContextService.FirstItemKey.ToInt() };
            (_areaContextService.GetFirstDicItem()[AreaContextService.KeyTz] as List<object>)?.Add(ptw);
            RecursiveAdd(array, twCode);

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