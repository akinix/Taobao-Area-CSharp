using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taobao.Area.Api.Configurations
{
    public class TaobaoAreaSettings
    {
        public string TempDirectoryName { get; set; }

        public string TaobaoJsVersion { get; set; }
        
        public string TaobaoAreaJsUrl { get; set; }

        public string JsTemplate { get; set; }

        public string AreaPickerDataJsName { get; set; }

        public string TaobaoStreetUrl { get; set; }

        public string JsDirectoryName { get; set; }
    }
}
