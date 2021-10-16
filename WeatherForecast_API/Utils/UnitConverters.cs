using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast_API.Utils
{
    public class UnitConverters
    {
        public static decimal KelvinToCelsius(decimal kelvinDegree) => kelvinDegree - 273.15M;
    }
}
