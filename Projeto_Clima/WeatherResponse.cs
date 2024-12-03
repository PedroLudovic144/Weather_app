using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Clima
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public string Name { get; set; }
    }
    public class Main
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }
}

