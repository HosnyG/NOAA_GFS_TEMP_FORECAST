using Amazon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherForecast_API.Infrastructure.Exceptions;
using WeatherForecast_API.Services;
using WeatherForecast_API.Services.Forecast;
using WeatherForecast_API.Utils;
using Wgrib2_Operator_NS;
using Wgrib2_Operator_NS.Exceptions;

namespace WeatherForecast_API.Controllers
{
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            this._forecastService = forecastService;
        }


        [Route("forecast/{date}/{lat}/{lon}")]
        [HttpGet]
        public async Task<IActionResult> Forecast([FromRoute] DateTime date, double lat, double lon)
        {
            try
            {
                TempratureForecastResponse response = await _forecastService.GetTempratureForecast(date, lat, lon, metersAboveGround: 2);
                return Ok(response);
            }
            catch (APIException ex)
            {
                if (Statics.IsDevelopment())
                    return StatusCode(ex.StatusCode, ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                if (Statics.IsDevelopment())
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
