using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast_API.Services.Forecast
{
    public class ForecastFormatResource
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Offset { get; set; }
    }
}
