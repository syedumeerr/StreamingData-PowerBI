using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureSensors
{
   class program
    {
        static HttpClient client = new HttpClient();
        static string powerBiPostUrl = "https://api.powerbi.com/beta/fee3b916-01c1-4987-a646-e193432b9eaa/datasets/6ed228c3-bb07-4db2-8386-c193c5e15391/rows?redirectedFromSignup=1%2C1%2C1&experience=power-bi&key=fHO%2FiZZj0lzesrxLEQOJAQC75x%2FI%2BXukHYIj358pN43GMbztKelmmgeHmKh%2Fu1WqeUTNpggHjElwjOfvCBSIiA%3D%3D";
        static Random randomtemp = new Random();

        static void Main (string[] args)
        {
            while (true)
            {
                var rValue = randomtemp.NextDouble() * (23.5
                -
                24.0) + 23.5;
                var tempDate = new Temp()
                {
                    ZoneName = "Zone A",
                    DateTime = DateTime.Now,
                    Temperature = (decimal)rValue
                };
                var jsonString = JsonConvert.SerializeObject(tempDate);
                var postToPowerBi = HttpPostAsync(powerBiPostUrl, "[" + jsonString + "]"); // Add brackets for Power BI
                Console.WriteLine(jsonString);
                System.Threading.Thread.Sleep(1000);
            }

            }

        static async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;



        }

    }
    
}
