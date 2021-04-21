using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Helpers
{
    public class ContainerHelper
    {
        public static string[] CommandStringToArray(string command)
        {
            var pattern = new Regex("(\\s)\\1+");

            return pattern.Replace(command, " ").Split(' ');
        }
    }
}
