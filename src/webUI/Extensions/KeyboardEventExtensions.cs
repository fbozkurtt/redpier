using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Extensions
{
    public static class KeyboardEventExtensions
    {
        public static bool TryGetChar(this KeyboardEventArgs e, out char value)
        {
            value = default;
            if (e.Key.Equals("Enter") && e.Type.Equals("keypress"))
            {
                value = '\n';
                return true;
            }
            if (e.Key.Equals("Tab"))
            {
                value = (char)9;
                return true;
            }
            if (e.Key.Equals("Backspace"))
            {
                value = (char)127;
                return true;
            }
            if (e.CtrlKey == false && e.Key.Length.Equals(1) && e.Type.Equals("keypress"))
            {
                value = Convert.ToChar(e.Key);
                return true;
            }
            else
            {
                if (e.CtrlKey)
                {
                    e.Key = e.Key.ToUpperInvariant();
                    if (e.Key == "@")
                        value = (char)0;
                    if (e.Key == "A")
                        value = (char)1;
                    if (e.Key == "B")
                        value = (char)2;
                    if (e.Key == "C")
                        value = (char)3;
                    if (e.Key == "D")
                        value = (char)4;
                    if (e.Key == "E")
                        value = (char)5;
                    if (e.Key == "F")
                        value = (char)6;
                    if (e.Key == "G")
                        value = (char)7;
                    if (e.Key == "H")
                        value = (char)8;
                    if (e.Key == "I")
                        value = (char)9;
                    if (e.Key == "J")
                        value = (char)10;
                    if (e.Key == "K")
                        value = (char)11;
                    if (e.Key == "L")
                        value = (char)12;
                    if (e.Key == "M")
                        value = (char)13;
                    if (e.Key == "N")
                        value = (char)14;
                    if (e.Key == "O")
                        value = (char)15;
                    if (e.Key == "P")
                        value = (char)16;
                    if (e.Key == "Q")
                        value = (char)17;
                    if (e.Key == "R")
                        value = (char)18;
                    if (e.Key == "S")
                        value = (char)19;
                    if (e.Key == "T")
                        value = (char)20;
                    if (e.Key == "U")
                        value = (char)21;
                    if (e.Key == "V")
                        value = (char)22;
                    if (e.Key == "W")
                        value = (char)23;
                    if (e.Key == "X")
                        value = (char)24;
                    if (e.Key == "Y")
                        value = (char)25;
                    if (e.Key == "Z")
                        value = (char)26;
                    if (e.Key == "[")
                        value = (char)27;
                    if (e.Key == "\\")
                        value = (char)28;
                    if (e.Key == "]")
                        value = (char)29;
                    if (e.Key == "^")
                        value = (char)30;
                    if (e.Key == "_")
                        value = (char)31;
                    return true;
                }
            }
            return false;
        }
    }
}
