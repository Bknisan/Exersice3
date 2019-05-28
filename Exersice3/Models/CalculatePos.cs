using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exersice3.Models
{
    public class CalculatePos
    {
        // norm val
        public static double normLongi(double longi)
        {
            return longi / 18 * 10;
        }
        // norm val
        public static double normLati(double lati)
        {
            return lati / 9 * 10;
        }
    }
}