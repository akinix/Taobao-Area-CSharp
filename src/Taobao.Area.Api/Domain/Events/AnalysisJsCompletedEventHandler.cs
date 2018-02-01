using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Taobao.Area.Api.Domain.Commands;

namespace Taobao.Area.Api.Domain.Events
{
    public class AnalysisJsCompletedEventHandler : INotificationHandler<AnalysisJsCompletedEvent>
    {
        private readonly IMediator _mediator;

        public AnalysisJsCompletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(AnalysisJsCompletedEvent @event, CancellationToken cancellationToken)
        {
            //交给 CreateDataJsCommand 命令去处理
            await _mediator.Send(new CreateDataJsCommand(), cancellationToken);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
