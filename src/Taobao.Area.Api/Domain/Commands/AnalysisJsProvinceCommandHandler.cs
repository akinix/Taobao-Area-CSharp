using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Taobao.Area.Api.Domain.Events;
using Taobao.Area.Api.Domain.Services;
using Taobao.Area.Api.Events;
using Taobao.Area.Api.Extensions;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisJsProvinceCommandHandler : IRequestHandler<AnalysisJsProvinceCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly AreaContextService _areaContextService;

        public AnalysisJsProvinceCommandHandler(
            IMediator mediator,
            AreaContextService areaContextService)
        {
            _mediator = mediator;
            _areaContextService = areaContextService;
        }

        public async Task<bool> Handle(AnalysisJsProvinceCommand command, CancellationToken cancellationToken)
        {
            var dicProvince = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(_areaContextService.ProvinceString);
            var jaArea = JsonConvert.DeserializeObject<JArray>(_areaContextService.AreaString);

            foreach (var key in dicProvince.Keys)
            {
                var rows = new List<object>();
                foreach (var obj in dicProvince[key])
                {
                    var code = obj[0].Value<string>();
                    var props = new List<object> { code.ToInt(), obj[1][0].Value<string>(), AreaContextService.FirstItemKey.ToInt() };
                    if (code == "110000")
                        rows.Insert(0, props);//确保北京在第一个
                    else
                        rows.Add(props);

                    RecursiveAdd(jaArea, code);
                }
                _areaContextService.GetFirstDicItem().TryAdd(key, rows);
            }
            return true;
        }

        private void RecursiveAdd(JArray ja, string pid)
        {
            var subs = ja.Where(i => i[2].Value<string>() == pid).ToList();

            if (subs.Count == 0)
            {
                //末级节点 触发 DistrictAddedEvent 事件
                try
                {
                    var districtCode = pid;
                    var p = ja.FirstOrDefault(i => i[0].Value<string>() == pid);//有些城市下无区县
                    if (p == null)
                        return;
                    var pId = p[2].Value<string>();
                    var pp = ja.FirstOrDefault(i => i[0].Value<string>() == pId);
                    if (pp == null)
                        return;
                    var ppId = pp[2].Value<string>();

                    //_mediator.Publish(new DistrictAddedEvent(ppId, pId, districtCode));
                    return;
                }
                catch (Exception ex)
                {

                }
            }
            var rows = new List<object>();
            foreach (var sub in subs)
            {
                var subid = sub[0].Value<string>();
                var dataType = sub[3].Value<string>().ToInt();
                List<object> props;
                if (dataType == 0)
                {
                    props = new List<object> { subid.ToInt(), sub[1][0].Value<string>(), pid.ToInt() };
                }
                else if (dataType == 3)
                {
                    // 仿照淘宝 只取类型 = 3 属于/已合并到/已更名为
                    var fullname = sub[1][0].Value<string>();
                    var names = fullname.Split(" ");
                    props = new List<object> { subid.ToInt(), names[0], pid.ToInt(), 3, names[1], names[2] };
                }
                else
                {
                    continue;
                }
                rows.Add(props);
                // subid 可能为 "210211,210212" 这种格式 就无需处理
                if (subid.IndexOf(',') > 0)
                    continue;
                RecursiveAdd(ja, subid);
            }

            _areaContextService.MainDictionary.TryAdd(pid, rows);
        }
    }
}