using System;
using System.Collections.Generic;
using System.Text;

namespace Wgrib2_Operator_NS.Exceptions
{
    public class RunAppException : Exception
    {
        public string StandardError { get; set; }
        public int ExitCode { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset ExitTime { get; set; }
        public RunAppException() { }
        public RunAppException(string message) : base(message) { }
        public RunAppException(string message, string standardError, int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime) : base(message)
        {
            this.StandardError = standardError;
            this.ExitCode = exitCode;
            this.StartTime = StartTime;
            this.ExitTime = exitTime;
        }
    }
}
