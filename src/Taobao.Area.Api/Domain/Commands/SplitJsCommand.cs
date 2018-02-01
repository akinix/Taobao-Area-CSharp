using MediatR;

namespace Taobao.Area.Api.Domain.Commands
{
    /// <summary>
    /// 拆分Js命令参数
    /// </summary>
    public class SplitJsCommand : IRequest
    {
        /// <summary>
        /// 下载后的临时淘宝Js文件全路径
        /// </summary>
        public string TempJsName { get; }

        public SplitJsCommand(string tempJsName)
        {
            TempJsName = tempJsName;
        }
    }
}