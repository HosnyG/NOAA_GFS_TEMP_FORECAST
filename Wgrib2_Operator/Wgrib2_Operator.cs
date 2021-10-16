using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wgrib2_Operator_NS;

namespace Wgrib2_Operator_NS
{
    public class Wgrib2_Operator : IWgrib2_Operator
    {
        private const string _appName = "wgrib2";
        private readonly string _appExePath;

        public Wgrib2_Operator(string appExePath)
        {
            this._appExePath = appExePath;
        }

        /// <summary>
        /// Run wgrib2 exe application async with the specified file and params to  get temprature forecast.
        /// </summary>
        /// <returns>structured class for lon,lat,temprtaure in klevin</returns>
        public async Task<TempratureOutputResource> GetTempratureForecastAsync(string wgrib2File,double lat,double lon,int metersAboveGround = 2)
        {
            string args = $"-match \":(TMP:{metersAboveGround} m above ground):\" -lon {lon} {lat}";
            string command = wgrib2File + " " + args;
            string output =  await RunAndGetOutputAsync(command);
            return Wgrib2ResponseParser.ParseTempratureForecastOutput(output);
        }

        /// <summary>
        /// Run wgrib2 exe application async with a given command.
        /// </summary>
        /// <param name="command">app command</param>
        /// <returns>stdout message</returns>
        private async Task<string> RunAndGetOutputAsync(string command)
        {
            return await RunEXEApps.RunEXEAppAndGetOutputAsync(this._appExePath, command, _appName);
        }

    }
}
