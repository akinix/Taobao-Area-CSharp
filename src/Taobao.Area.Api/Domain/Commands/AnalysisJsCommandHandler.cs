using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Taobao.Area.Api.Domain.Events;
using Taobao.Area.Api.Domain.Services;
using Taobao.Area.Api.Exceptions;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisJsCommandHandler : IRequestHandler<AnalysisJsCommand>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AnalysisJsCommandHandler> _logger;

        public AnalysisJsCommandHandler(
            IMediator mediator,
            ILogger<AnalysisJsCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(AnalysisJsCommand command, CancellationToken cancellationToken)
        {
            var result = false;
            result = await _mediator.Send(new AnalysisJsNoneDistrictCityCommand(), cancellationToken);
            if (!result)
            {
                _logger.LogInformation($"命令{nameof(AnalysisJsNoneDistrictCityCommand)}执行失败。");
            }
            result = await _mediator.Send(new AnalysisJsProvinceCommand(), cancellationToken);
            if (!result)
            {
                _logger.LogInformation($"命令{nameof(AnalysisJsProvinceCommand)}执行失败。");
            }
            result = await _mediator.Send(new AnalysisJsGangAoCommand(), cancellationToken);
            if (!result)
            {
                _logger.LogInformation($"命令{nameof(AnalysisJsGangAoCommand)}执行失败。");
            }
            result = await _mediator.Send(new AnalysisJsTaiwanCommand(), cancellationToken);
            if (!result)
            {
                _logger.LogInformation($"命令{nameof(AnalysisJsTaiwanCommand)}执行失败。");
            }
            await _mediator.Publish(new AnalysisJsCompletedEvent(), cancellationToken);
        }
    }
}
