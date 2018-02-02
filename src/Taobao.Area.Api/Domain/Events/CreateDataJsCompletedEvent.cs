using MediatR;

namespace Taobao.Area.Api.Domain.Events
{
    public class CreateDataJsCompletedEvent : INotification
    {
        public string DataJsName { get; }

        public CreateDataJsCompletedEvent(string dataJsName)
        {
            DataJsName = dataJsName;
        }
    }
}
