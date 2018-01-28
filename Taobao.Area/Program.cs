using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Taobao.Area
{
    class Program
    {
        static void Main(string[] args)
        {

            //var services = new ServiceCollection()
            //    .AddOptions()
            //    .AddLogging()

            //var config = new ConfigurationBuilder()
            //    .AddInMemoryCollection()    //将配置文件的数据加载到内存中
            //    .SetBasePath(Directory.GetCurrentDirectory())   //指定配置文件所在的目录
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  //指定加载的配置文件
            //    .Build();    //编译成对象

            //var loggerFactory = new LoggerFactory()
            //    .AddConsole();







            //var temp = Directory.GetCurrentDirectory();
            //var uri = "https://g.alicdn.com/vip/address/6.0.14/index-min.js";
            //var service = new AnalysisService(uri);
            //var result = service.Process().GetAwaiter().GetResult();

            Console.ReadKey();
        }
    }

    public class AnalysisService
    {
        private readonly string _jsUri;

        public AnalysisService(string jsUri)
        {
            _jsUri = jsUri;
        }

        public async Task<bool> Process()
        {
            var jsContext = await Download();


            return true;
        }


        private async Task<string> Download()
        {
            var _client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _jsUri);
            var response = await _client.SendAsync(requestMessage);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"下载 {_jsUri} 失败。");
                return string.Empty;
            }
                
            var context = await response.Content.ReadAsStringAsync();

            return string.Empty;
        }
    }
}