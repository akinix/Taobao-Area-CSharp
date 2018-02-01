using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Taobao.Area.Api.Domain.Services;

namespace Taobao.Area.Api.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AreaContextService())
                .As<AreaContextService>()
                .InstancePerLifetimeScope();
        }
    }
}
