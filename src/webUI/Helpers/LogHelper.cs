using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Helpers
{
    public class LogHelper
    {
        public static string[] FormatLogs(string logs, bool skipHeaders, bool includeTimeStamps = false)
        {
            //logs = Regex.Replace(logs, "/[\u001b\u009b][[()#;?]*(?:[0-9]{1,4}(?:;[0-9]{0,4})*)?[0-9A-ORZcf-nqry=><]/g", "");

            var lines = logs.Split('\n');

            if (skipHeaders)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length >= 8)
                        lines[i] = lines[i].Substring(8);
                }
            }
            if (includeTimeStamps)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length >= 8)
                    {
                        var tokens = lines[i].Split(' ');
                        var timeStamp = tokens.First();

                        if(DateTime.TryParse(timeStamp, out var time))
                        {
                            tokens[0] = time.ToString("yyyy-MM-dd HH:mm");
                        }

                        lines[i] = string.Join(' ', tokens);
                        Console.WriteLine(lines[i]);
                    }
                }
            }

            return lines;
        }
    }
}