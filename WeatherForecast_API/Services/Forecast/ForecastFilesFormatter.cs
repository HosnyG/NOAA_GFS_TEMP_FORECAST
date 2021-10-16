using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast_API.Services.Forecast
{
    public static class ForecastFilesFormatter
    {
        private const string bucketForecastFilesPath = "gfs.{0}{1}{2}/00/atmos/gfs.t00z.pgrb2.0p25.f{3}";

        public static string GetClosestForecastFileName(DateTime date)
        {
            ForecastFormatResource ff = GetClosestForecastFormatForDate(date);
            return String.Format(bucketForecastFilesPath, ff.Year, ff.Month.ToString("00"), ff.Day.ToString("00"), ff.Offset.ToString("000"));
        }


        private static ForecastFormatResource GetClosestForecastFormatForDate(DateTime date)
        {
            DateTime filteredDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0); //without minutes, seconds;
            //passed date -> look for a file with the same date & hour(as offset)
            if (filteredDate < DateTime.Now) 
            {
                return new ForecastFormatResource { Year = filteredDate.Year, Month = filteredDate.Month, Day = filteredDate.Day, Offset = filteredDate.Hour };
            }
            //future -> look for a file with current date and set the offset as the difference between the given date and current date
            else
            {
                DateTime now = DateTime.Now;
                int offset = (int)(filteredDate - now.Date).TotalHours;
                return new ForecastFormatResource { Year = now.Year, Month = now.Month, Day = now.Day, Offset = offset };
            }
        }

    }
}
