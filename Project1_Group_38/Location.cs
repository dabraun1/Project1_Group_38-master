using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Group_38
{
    public struct Location
    {
        public decimal Latitude, Longitude;
        public Location(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Overridden ToString
        /// </summary>
        /// <By>David Aaron Braun</By>
        /// <returns></returns>
        public override string ToString()
        {
            return Latitude + " * " + Longitude;
        }//End of ToString()
    }
}
