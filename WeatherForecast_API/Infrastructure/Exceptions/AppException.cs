using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast_API.Utils;

namespace WeatherForecast_API.Infrastructure.Exceptions
{
    public class APIException : Exception
    {
        public int StatusCode { get; set; }
        public APIException(string message) : base(message)
        {
            this.StatusCode = StatusCodes.Status500InternalServerError;
        }
        public APIException(int statusCode,string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

    }
}
