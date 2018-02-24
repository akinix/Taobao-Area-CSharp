using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Taobao.Area.Api.Domain.Commands;

namespace Taobao.Area.Api.Domain.Events
{
    public class NoneDistrictCityAddedEventHandler : INotificationHandler<NoneDistrictCityAddedEvent>
    {
        private readonly IMediator _mediator;

        public NoneDistrictCityAddedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(NoneDistrictCityAddedEvent @event, CancellationToken cancellationToken)
        {
            // 交给 下载街道命令去处理 
            var cmd = new DownloadStreetDataCommand(@event.ProvinceCode, @event.CityCode, string.Empty);
            await _mediator.Send(cmd, cancellationToken);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
