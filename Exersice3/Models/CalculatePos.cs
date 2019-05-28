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
            return longi / 180;
        }
        // norm val
        public static double normLati(double lati)
        {
            return lati / 360;
        }
    }
}