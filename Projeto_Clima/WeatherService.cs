using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Clima
{
    public class WeatherService
    {
        private const string ApiKey = "adc3136478ba1b7b728b05af03d861f1\r\n";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<WeatherResponse> GetWeatherByCity(string city)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync($"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=pt_br");
            return JsonConvert.DeserializeObject<WeatherResponse>(response);
        }
    }
}
