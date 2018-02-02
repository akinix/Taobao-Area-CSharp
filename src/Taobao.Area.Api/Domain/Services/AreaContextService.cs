using System.Collections.Generic;

namespace Taobao.Area.Api.Domain.Services
{
    public class AreaContextService
    {
        public const string FirstItemKey = "1";
        public const string KeyAg = "A-G";
        public const string KeyTz = "T-Z";

        public AreaContextService()
        {
            MainDictionary = new Dictionary<string, object>();
            IsForce = false;
            Init();
        }

        public Dictionary<string, object> MainDictionary { get; set; }

        public string ProvinceString { get; private set; }

        public string GangAoString { get; private set; }

        public string TaiwanString { get; private set; }

        public string AreaString { get; private set; }

        public bool IsForce { get; private set; }

        public void SetProvinceString(string value)
        {
            ProvinceString = value;
        }

        public void SetGangAoString(string value)
        {
            GangAoString = value;
        }

        public void SetTaiwanString(string value)
        {
            TaiwanString = value;
        }

        public void SetAreaString(string value)
        {
            AreaString = value;
        }

        public void SetIsForce(bool value)
        {
            IsForce = value;
        }

        public bool HasAllString()
        {
            if (string.IsNullOrEmpty(ProvinceString)
                || string.IsNullOrEmpty(GangAoString)
                || string.IsNullOrEmpty(TaiwanString)
                || string.IsNullOrEmpty(AreaString))
                return false;

            return true;
        }

        private void Init()
        {
            MainDictionary.Add("1", new Dictionary<string, object>());
        }

        public Dictionary<string, object> GetFirstDicItem()
        {
            return GetDicItem(FirstItemKey);
        }

        public Dictionary<string, object> GetDicItem(string key)
        {
            return MainDictionary[key] as Dictionary<string, object>;
        }

        
    }
}
