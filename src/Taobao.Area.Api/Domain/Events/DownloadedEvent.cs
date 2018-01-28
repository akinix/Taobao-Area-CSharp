using MediatR;

namespace Taobao.Area.Api.Events
{
    public class DownloadedEvent : INotification
    {
        public string TempJsName { get; }

        public DownloadedEvent(string tempJsName)
        {
            TempJsName = tempJsName;
        }
    }
}
