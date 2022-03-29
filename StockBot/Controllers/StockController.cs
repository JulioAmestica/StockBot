using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public string Get(string stock_code)
        {
            JObject message = GetData(stock_code);
            try
            {
                return message["symbols"][0]["close"].ToString();
            }
            catch
            {
                return "Error";
            }
        }

        private JObject GetData(string stock_code)
        {
            HttpClient http = new HttpClient();
            string url = $"https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=json";
            JObject data = JObject.Parse(http.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            return data;
        }
    }
}
