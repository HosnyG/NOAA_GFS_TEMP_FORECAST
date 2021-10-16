using CliWrap;
using CliWrap.Buffered;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wgrib2_Operator_NS.Exceptions;

namespace Wgrib2_Operator_NS
{
    public static class RunEXEApps
    {
        /// <summary>
        /// Run exe app async and get the output (stdout)
        /// </summary>
        /// <returns>app output as string</returns>
        /// <exception cref="RunAppException"></exception>
        public static async Task<string> RunEXEAppAndGetOutputAsync(string appPath,string command, string appName = "")
        {
            var result = await Cli.Wrap(appPath)
                      .WithArguments(command)
                      .WithValidation(CommandResultValidation.None)
                      .ExecuteBufferedAsync();

            if (!string.IsNullOrEmpty(result.StandardError))
            {
                throw new RunAppException(message : $"error occurred while trying to run {appName} proccess",
                                          standardError: result.StandardError,
                                          exitCode:  result.ExitCode,
                                          startTime: result.StartTime,
                                          exitTime : result.ExitTime);
            }
            return result.StandardOutput;
        }
    }
}
