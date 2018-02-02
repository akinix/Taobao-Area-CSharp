using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Taobao.Area.Api.Domain.Commands;

namespace Taobao.Area.Api.Domain.Events
{
    public class DownloadJsCompletedEventHandler : INotificationHandler<DownloadJsCompletedEvent>
    {
        private readonly IMediator _mediator;

        public DownloadJsCompletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(DownloadJsCompletedEvent @event, CancellationToken cancellationToken)
        {
            //交给 分析命令去处理 
            await _mediator.Send(new SplitJsCommand(@event.TempJsName), cancellationToken);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
