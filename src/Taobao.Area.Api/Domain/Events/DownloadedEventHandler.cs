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
    public class DownloadedEventHandler : INotificationHandler<DownloadedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DownloadedEventHandler> _logger;

        public DownloadedEventHandler(IMediator mediator, ILogger<DownloadedEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(DownloadedEvent downloadedEvent, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"接收到Js下载完成事件，Js临时目录 : {downloadedEvent.TempJsName}。");

            //交给 分析命令去处理 
            var cmd = new AnalysisCommand(downloadedEvent.TempJsName);
            await _mediator.Send(cmd);

            // 如果有其他消费者 在此添加
            // ...
        }
    }
}
