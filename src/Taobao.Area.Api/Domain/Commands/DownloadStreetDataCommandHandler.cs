using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using Taobao.Area.Api.Domain.Jobs;
using Taobao.Area.Api.Domain.Services;

namespace Taobao.Area.Api.Domain.Commands
{
    public class DownloadStreetDataCommandHandler : IRequestHandler<DownloadStreetDataCommand>
    {
        private readonly AreaContextService _areaContextService;
        private readonly ILogger<DownloadStreetDataCommandHandler> _logger;

        public DownloadStreetDataCommandHandler(
            AreaContextService areaContextService,
            ILogger<DownloadStreetDataCommandHandler> logger)
        {
            _areaContextService = areaContextService;
            _logger = logger;
        }

        public async Task Handle(DownloadStreetDataCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"开始创建获取街道后台任务。{nameof(command.DistrictCode)}:{command.DistrictCode}");
            BackgroundJob.Enqueue<DownloadStreetDataJob>(x => x.DownloadAsync(command.ProvinceCode, command.CityCode, command.DistrictCode, _areaContextService.IsForce));
            _logger.LogInformation($"完成创建获取街道后台任务。{nameof(command.DistrictCode)}:{command.DistrictCode}");
        }
    }
}
