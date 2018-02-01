using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
