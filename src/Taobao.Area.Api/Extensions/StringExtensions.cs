using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taobao.Area.Api.Extensions
{
    public static class StringExtensions
    {
        /// <summary>  
        /// 删除最后结尾的指定字符后的字符  
        /// </summary>  
        public static string Rtrim(this string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        /// <summary>  
        /// 删除最前开头的指定字符后的字符  
        /// </summary>  
        public static string Ltrim(this string str, string strchar)
        {
            var index = str.IndexOf(strchar);
            if (index < 0)
                return str;
            var startIndex = index + strchar.Length;
            return str.Substring(startIndex);
        }

        public static int ToInt(this string str)
        {
            var res = 0;
            int.TryParse(str, out res);
            return res;
        }

    }
}
