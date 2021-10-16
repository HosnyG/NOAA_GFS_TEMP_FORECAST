using Amazon;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast_API.Infrastructure.Exceptions;
using WeatherForecast_API.Services.Forecast;
using WeatherForecast_API.Utils;
using Wgrib2_Operator_NS;

namespace WeatherForecast_API.Services
{
    public class GFS_ForecastService : IForecastService
    {
        private const int _supportedHoursInAdvance = 120;
        private readonly string _s3BucketName;
        private readonly string _grb2filesDirectoryPath;
        private readonly RegionEndpoint _region;
        private readonly IWgrib2_Operator _wgrib2_Operator;
        public GFS_ForecastService(IWgrib2_Operator wgrib2_operator,string s3bucketName, string grb2filesDirectoryPath,string region)
        {
            this._wgrib2_Operator = wgrib2_operator;
            this._s3BucketName = s3bucketName;
            this._grb2filesDirectoryPath = grb2filesDirectoryPath;
            this._region = Amazon.RegionEndpoint.GetBySystemName(region);
        }

        /// <summary>
        /// Get temprature forecast according to NOAA GFS for a specific date and location
        /// </summary>
        public async Task<TempratureForecastResponse> GetTempratureForecast(DateTime date,double lat, double lon, int metersAboveGround)
        {
            //date validaion
            if (!IsSupportedDate(date))
                throw new APIException(StatusCodes.Status400BadRequest, $"unsupported date : until {_supportedHoursInAdvance} hours in advance forecasts supported");
            //build closest forecast file name (as it's specified in s3 bucket)
            string forecastFileInS3 = ForecastFilesFormatter.GetClosestForecastFileName(date);
            //file name in local machine
            string storedforecastFilePath = Path.Combine(this._grb2filesDirectoryPath, forecastFileInS3.Replace("/","_"));
            if (!File.Exists(storedforecastFilePath)) // download from s3 bucket
            {
                await S3FileDownloader.DownloadFileAsync(_s3BucketName, forecastFileInS3, storedforecastFilePath, _region, string.Empty, string.Empty);
            }
            //run wgrib2 exe app to get the temprature forecsast
            TempratureOutputResource forecastResponse = await _wgrib2_Operator.GetTempratureForecastAsync(storedforecastFilePath, lat, lon,metersAboveGround);
            //format the response
            return new TempratureForecastResponse { Latitude = forecastResponse.Latitude,
                                                    Longitude =forecastResponse.Longitude,
                                                    TemperatureKelvin = forecastResponse.TempratureKelvin,
                                                    TemperatureCelsius = UnitConverters.KelvinToCelsius(forecastResponse.TempratureKelvin) };
        }

        private bool IsSupportedDate(DateTime date)
        {
            return (date - DateTime.Now).TotalHours <= _supportedHoursInAdvance;
        }
    }
}
