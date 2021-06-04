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
                    {
                        value = (char)0;
                        return true;
                    }
                    if (e.Key == "A")
                    {
                        value = (char)1;
                        return true;
                    }
                    if (e.Key == "B")
                    {
                        value = (char)2;
                        return true;
                    }
                    if (e.Key == "C")
                    {
                        value = (char)3;
                        return true;
                    }
                    if (e.Key == "D")
                    {
                        value = (char)4;
                        return true;
                    }
                    if (e.Key == "E")
                    {
                        value = (char)5;
                        return true;
                    }
                    if (e.Key == "F")
                    {
                        value = (char)6;
                        return true;
                    }
                    if (e.Key == "G")
                    {
                        value = (char)7;
                        return true;
                    }
                    if (e.Key == "H")
                    {
                        value = (char)8;
                        return true;
                    }
                    if (e.Key == "I")
                    {
                        value = (char)9;
                        return true;
                    }
                    if (e.Key == "J")
                    {
                        value = (char)10;
                        return true;
                    }
                    if (e.Key == "K")
                    {
                        value = (char)11;
                        return true;
                    }
                    if (e.Key == "L")
                    {
                        value = (char)12;
                        return true;
                    }
                    if (e.Key == "M")
                    {
                        value = (char)13;
                        return true;
                    }
                    if (e.Key == "N")
                    {
                        value = (char)14;
                        return true;
                    }
                    if (e.Key == "O")
                    {
                        value = (char)15;
                        return true;
                    }
                    if (e.Key == "P")
                    {
                        value = (char)16;
                        return true;
                    }
                    if (e.Key == "Q")
                    {
                        value = (char)17;
                        return true;
                    }
                    if (e.Key == "R")
                    {
                        value = (char)18;
                        return true;
                    }
                    if (e.Key == "S")
                    {
                        value = (char)19;
                        return true;
                    }
                    if (e.Key == "T")
                    {
                        value = (char)20;
                        return true;
                    }
                    if (e.Key == "U")
                    {
                        value = (char)21;
                        return true;
                    }
                    if (e.Key == "V")
                    {
                        value = (char)22;
                        return true;
                    }
                    if (e.Key == "W")
                    {
                        value = (char)23;
                        return true;
                    }
                    if (e.Key == "X")
                    {
                        value = (char)24;
                        return true;
                    }
                    if (e.Key == "Y")
                    {
                        value = (char)25;
                        return true;
                    }
                    if (e.Key == "Z")
                    {
                        value = (char)26;
                        return true;
                    }
                    if (e.Key == "[")
                    {
                        value = (char)27;
                        return true;
                    }
                    if (e.Key == "\\")
                    {
                        value = (char)28;
                        return true;
                    }
                    if (e.Key == "]")
                    {
                        value = (char)29;
                        return true;
                    }
                    if (e.Key == "^")
                    {
                        value = (char)30;
                        return true;
                    }
                    if (e.Key == "_")
                    {
                        value = (char)31;
                        return true;
                    }
                }
            }
            value = default;
            return false;
        }
    }
}
