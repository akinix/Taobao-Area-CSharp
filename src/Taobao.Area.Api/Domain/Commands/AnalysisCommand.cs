using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taobao.Area.Api.Domain.Commands
{
    public class AnalysisCommand : IRequest<string>
    {
        public string TempJsName { get; private set; }

        public AnalysisCommand(string tempJsName)
        {
            TempJsName = tempJsName;
        }
    }
}
