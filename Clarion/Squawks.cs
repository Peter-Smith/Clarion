using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// this should likely use datafiles eventually, but this basically serves as langage command for Clarion
// IE, it tells how battle event messages should be composed

namespace Clarion
{
    public static class Squawks
    {

        public static string Mfates(int f, int r) {
            string result;
            result = "Turn begins. ";
            result += "Fate: " + f+" ";
            result += "Rising: " + r + " ";
            return result;
        }

    }
}