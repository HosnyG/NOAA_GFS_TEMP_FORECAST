using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast_API.Services.Forecast
{
    public class TempratureForecastResponse
    {
        public decimal TemperatureCelsius { get; set; }
        public decimal TemperatureKelvin { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}
