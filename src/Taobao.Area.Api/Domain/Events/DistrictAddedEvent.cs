using MediatR;

namespace Taobao.Area.Api.Domain.Events
{
    public class DistrictAddedEvent : INotification
    {
        public string ProvinceCode { get; }

        public string CityCode { get; }

        public string DistrictCode { get; }

        public DistrictAddedEvent(string provinceCode, string cityCode, string districtCode)
        {
            ProvinceCode = provinceCode;
            CityCode = cityCode;
            DistrictCode = districtCode;
        }
    }
}
