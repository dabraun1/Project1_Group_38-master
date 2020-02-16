/*
 * Project 1 - Group 38 Statistics Class
 * David Aaron Braun
 * 2020-02-15
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Group_38
{
    public class Statistics
    {
        //Store the retrieved cityinfo
        public Dictionary<string, CityInfo> CityCatalogue;

        /// <summary>
        /// Construct Statics object
        /// </summary>
        /// <param name="dict"></param>
        public Statistics(Dictionary<string,CityInfo> dict)
        {
            CityCatalogue = dict;

            CityCatalogue.Remove("");

            //Debug
            for(int i = 0; i < CityCatalogue.Count(); i++)
            {
                Debug.WriteLine("City #" + i + " " + CityCatalogue.ElementAt(i).Value.CityName);
            }//End for
        }//End of constructor

        #region CityMethods

        /// <summary>
        /// Returns a CityInfo object if the name exists in the CityCatalogue
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public CityInfo GetCityByName(string n)
        {
            CityInfo err = new CityInfo();
            err.CityName = "Does not exist!";

            if(CityCatalogue.ContainsKey(n))
            {
                return CityCatalogue[n];
            }//End if
            else
            {
                return err;
            }//End else
        }//End of GetCityByName

        /// <summary>
        /// Returns the city with the lowest population within a province
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public CityInfo GetLargestPopulationCityByProvince(string n)
        {
            CityInfo bigcity = new CityInfo();

            for(int i = 0; i < CityCatalogue.Count; i++)
            {
                if(CityCatalogue.ElementAt(i).Value.Province == n)
                {
                    if(bigcity != CityCatalogue.ElementAt(i).Value
                    && CityCatalogue.ElementAt(i).Value.Population >
                    bigcity.Population)
                    {
                        bigcity = CityCatalogue.ElementAt(i).Value;
                    }//End if
                }//End if
            }//End for
            return bigcity;
        }//End of GetLargestPopulationCityByProvince


        /// <summary>
        /// Returns the city with the lowest population within a province
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public CityInfo GetSmallestPopulationCityByProvince(string n)
        {
            CityInfo smallcity = new CityInfo();
            //Debug
            smallcity.CityName = "Invalid";
            smallcity.Population = 1000000000000;

            for (int i = 0; i < CityCatalogue.Count; i++)
            {
                if (CityCatalogue.ElementAt(i).Value.Province == n)
                {
                    //Population is greater than 0
                    if (CityCatalogue.ElementAt(i).Value.Population > 0)
                    {
                        //Is Different than existing
                        if (CityCatalogue.ElementAt(i).Value !=                smallcity)
                        {
                            //Is less than existing
                            if (CityCatalogue.ElementAt(i).Value.Population < smallcity.Population)
                            {
                                smallcity = CityCatalogue.ElementAt(i).Value;

                                //Debug
                                Debug.WriteLine("Small City: " + smallcity.ToString());
                            }//End if
                        }//End if
                    }//End if
                }//End if
            }//End for
            return smallcity;
        }//End of GetSmallestPopulationCityByProvince

        /// <summary>
        /// Compares the population of 2 cities, returns the city with
        /// the greatest population
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public CityInfo ComparePopulation(CityInfo a, CityInfo b)
        {
            if(a.Population > b.Population)
            {
                return a;
            }//End if
            else
            {
                return b;
            }//End else
        }//End of ComparePopulation

        #endregion

        #region ProvinceMethods

        #endregion
    }//End of class
}//End of namespace
