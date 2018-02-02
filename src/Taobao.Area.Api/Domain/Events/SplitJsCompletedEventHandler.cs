using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Taobao.Area.Api.Domain.Commands;

namespace Taobao.Area.Api.Domain.Events
{
    public class SplitJsCompletedEventHandler : INotificationHandler<SplitJsCompletedEvent>
    {
        private readonly IMediator _mediator;

        public SplitJsCompletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SplitJsCompletedEvent @event, CancellationToken cancellationToken)
        {
            //交给 AnalysisJsCommand 命令去处理
            await _mediator.Send(new AnalysisJsCommand(), cancellationToken);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
