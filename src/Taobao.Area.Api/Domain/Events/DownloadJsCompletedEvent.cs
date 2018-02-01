using MediatR;

namespace Taobao.Area.Api.Events
{
    public class DownloadJsCompletedEvent : INotification
    {
        public string TempJsName { get; }

        public DownloadJsCompletedEvent(string tempJsName)
        {
            TempJsName = tempJsName;
        }
    }
}