using MediatR;

namespace Taobao.Area.Api.Domain.Events
{
    public class NoneDistrictCityAddedEvent : INotification
    {
        public string ProvinceCode { get; }

        public string CityCode { get; }

        public NoneDistrictCityAddedEvent(string provinceCode, string cityCode)
        {
            ProvinceCode = provinceCode;
            CityCode = cityCode;
        }
    }
}
