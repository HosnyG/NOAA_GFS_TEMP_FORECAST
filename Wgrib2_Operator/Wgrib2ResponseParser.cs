using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Wgrib2_Operator_NS
{
    public class Wgrib2ResponseParser
    {
        /// <summary>Extracts lon,lat,val values from a given assignments <c>string</c></summary>
        /// <param name="output">assignments string</param>
        /// <returns>structured class for lon,lat,val values</returns>
        public static TempratureOutputResource ParseTempratureForecastOutput(string output)
        {
            Dictionary<string, string> variables = GetVariables(output);
            string[] requiredVariables = { "lat", "lon", "val" };
            List<string> missingVariables = new List<string>();
            foreach (var requiredVar in requiredVariables)
                if (!variables.ContainsKey(requiredVar))
                    missingVariables.Add(requiredVar);
            if (missingVariables.Count > 0)
                throw new Exception($"error occurred while trying to parse Wgrib2 response - missing variables : {String.Join(' ',missingVariables)}");
            return new TempratureOutputResource { Longitude = Convert.ToDouble(variables["lon"]), Latitude = double.Parse(variables["lat"]), TempratureKelvin = decimal.Parse(variables["val"]), };
        }

        /// <summary>method to parse variables assignments separated with commas string</summary>
        /// <param name="s">assignments string</param>
        /// <returns>variables dictionary, (key:var name, value: value-string)</returns>
        /// <example> ForExample:
        /// <code>
        /// string s = "a=1234,b=hello,c=43.10";
        /// var d = GetVariables(s)
        /// </code>
        /// d will be <c>dictionary</c> with key-values : ("a","1234"),("b","hello"),("c","43.10")
        /// </example>
        private static Dictionary<string, string> GetVariables(string s)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            string filtered = Regex.Replace(s, @"\t|\n|\r", "");
            string[] assignments = s.Split(new string[] { ",", ":" }, StringSplitOptions.None);
            foreach (string assignment in assignments)
            {
                if (assignment.Contains("="))
                {
                    try
                    {
                        string[] splittor = assignment.Split("=", StringSplitOptions.RemoveEmptyEntries);
                        string key = splittor[0];
                        string value = splittor[1];
                        variables.Add(key, value);
                    }
                    catch
                    {

                    }
                }
            }
            return variables;
        }
    }
}
