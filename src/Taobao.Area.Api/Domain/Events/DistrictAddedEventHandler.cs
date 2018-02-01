using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Taobao.Area.Api.Domain.Commands;
using Taobao.Area.Api.Events;

namespace Taobao.Area.Api.Domain.Events
{
    public class DistrictAddedEventHandler : INotificationHandler<DistrictAddedEvent>
    {
        private readonly IMediator _mediator;

        public DistrictAddedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(DistrictAddedEvent @event, CancellationToken cancellationToken)
        {
            //交给 下载街道命令去处理 
            var cmd = new DownloadStreetDataCommand(@event.ProvinceCode, @event.CityCode, @event.DistrictCode);
            await _mediator.Send(cmd, cancellationToken);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
