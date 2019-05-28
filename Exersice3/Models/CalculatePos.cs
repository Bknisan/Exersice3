using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exersice3.Models
{
    public class CalculatePos
    {
        // norm val
        public static double normLongi(string longi)
        {
            return (Double.Parse(longi) / 180);
        }
        // norm val
        public static double normLati(string lati)
        {
            return (Double.Parse(lati) / 360);
        }
    }
}