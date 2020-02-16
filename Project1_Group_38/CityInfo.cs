using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Group_38
{
    public class CityInfo
    {
        public string CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public long Population { get; set; }
        public string Province { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string GetProvince()
        {
            return Province;
        }

        public long GetPopulation()
        {
            return Population;
        }

        public Location GetLocation()
        {
            return new Location(Latitude, Longitude);
        }

        public CityInfo(string cityID, string cityName, string cityAscii, long population, string province, decimal latitude, decimal longitude)
        {
            CityID = cityID;
            CityName = cityName;
            CityAscii = cityAscii;
            Population = population;
            Province = province;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Constructs a blank city object
        /// <By>David Aaron Braun</By>
        /// </summary>
        public CityInfo()
        {

        }//End of constructor

        /// <summary>
        /// Overriden ToString()
        /// </summary>
        /// <By>David Aaron Braun</By>
        /// <returns></returns>
        public override string ToString()
        {
            return CityName + ", "+
                Province+" ("+
            Population + ")";
        }//End of ToString()
    }
}
