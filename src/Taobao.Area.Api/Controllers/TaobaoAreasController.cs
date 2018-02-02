using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Taobao.Area.Api.Domain.Commands;
using Taobao.Area.Api.Domain.Services;

namespace Taobao.Area.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TaobaoAreasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly AreaContextService _areaContextService;

        public TaobaoAreasController(
            IMediator mediator,
            AreaContextService areaContextService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _areaContextService = areaContextService ?? throw new ArgumentNullException(nameof(areaContextService));
        }

        /// <summary>
        /// 生成地址相关数据，包括重新下载淘宝js和所有街道json
        /// </summary>
        /// <returns></returns>
        [Route("build")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Build()
        {
            var result = await _mediator.Send(new DownloadJsCommand());
            if (result)
                return Ok();
            return BadRequest();
        }

        /// <summary>
        /// 重新生成地址相关数据，包括重新下载淘宝js和所有街道json
        /// </summary>
        /// <returns></returns>
        [Route("ReBuild")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReBuild()
        {
            _areaContextService.SetIsForce(true);
            var result = await _mediator.Send(new DownloadJsCommand());
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
