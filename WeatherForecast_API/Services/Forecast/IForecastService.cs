using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast_API.Services.Forecast;

namespace WeatherForecast_API.Services
{
    public interface IForecastService
    {
        Task<TempratureForecastResponse> GetTempratureForecast(DateTime date, double lat, double lon, int metersAboveGround);
    }
}
