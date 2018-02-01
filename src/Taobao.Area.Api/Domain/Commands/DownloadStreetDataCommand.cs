using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Taobao.Area.Api.Domain.Commands
{
    public class DownloadStreetDataCommand: IRequest
    {
        public string ProvinceCode { get; private set; }
        public string CityCode { get; private set; }
        public string DistrictCode { get; private set; }

        public DownloadStreetDataCommand(string provinceCode, string cityCode, string districtCode)
        {
            ProvinceCode = provinceCode;
            CityCode = cityCode;
            DistrictCode = districtCode;
        }
    }
}