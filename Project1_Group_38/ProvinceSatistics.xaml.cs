/*
 * Project 1 - Group 38 Province Statistcs Class
 * David Aaron Braun
 * 2020-02-16
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project1_Group_38
{
    /// <summary>
    /// Temp Partial Class
    /// </summary>
    public class Province : IComparable
    {
        public string name;
        public long pop;
        public int numCities;

        /// <summary>
        /// Enable Sorting
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Province provToCompare = obj as Province;
            if (provToCompare.pop > pop || provToCompare.numCities > numCities)
            {
                return 1;
            }
            if (provToCompare.pop < pop || provToCompare.numCities < numCities)
            {
                return -1;
            }
            return 0;
        }//End of ENABLE SORTING

        /// <summary>
        /// Overriden ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name + " " + " (" + pop.ToString("N", CultureInfo.InvariantCulture) + ")";
        }//End of ToString()

        public string ToStringCitiesCount()
        {
            return name + " " + " (" + numCities.ToString("N", CultureInfo.InvariantCulture) + ")";
        }//End of ToStringCitiesCount()
    }//End of class
    /// <summary>
    /// Interaction logic for ProvinceSatistics.xaml
    /// </summary>
    public partial class ProvinceSatistics : Window
    {
        Statistics provStats;
        public ProvinceSatistics(Statistics ps)
        {
            provStats = ps;
            InitializeComponent();
        }//End of constructor

        /// <summary>
        /// Returns a Province with it's population
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Province GetPopByProvince(string name)
        {
            Province prov = new Province();
            long population = 0;

            for (int i = 0; i < provStats.CityCatalogue.Count(); i++)
            {
                if (provStats.CityCatalogue.ElementAt(i).Value.Province == name)
                {
                    population += provStats.CityCatalogue.ElementAt(i).Value.Population;
                }//End if
            }//End for

            prov.name = name;
            prov.pop = population;
            return prov;
        }//End of getPopByProvince();

        public Province GetNumCityByProvince(string name)
        {
            Province temp = new Province();
            temp.name = name;

            for (int i = 0; i < provStats.CityCatalogue.Count(); i++)
            {
                if (provStats.CityCatalogue.ElementAt(i).Value.Province == name)
                {
                    temp.numCities += 1;
                }//End if
            }//End for

            return temp;
        }//End of GetNumCityByProvince()

        private void sortPop(object sender, RoutedEventArgs e)
        {
            //Clear Previous
            lbxProv.ItemsSource = null;
            lbxProv.Items.Clear();
            lblTotals.Content = "";

            List<string> provNames = provStats.listProvinces();
            List<Province> provs = new List<Province>();

            for(int i = 0; i < provNames.Count(); i++)
            {
                Province temp = GetPopByProvince(provNames[i]);
                provs.Add(temp);
            }//End for

            provs.Sort();

            lbxProv.ItemsSource = provs;

        }//End of sortPop EVENT

        private void sortCities(object sender, RoutedEventArgs e)
        {
            List<string> provNames = provStats.listProvinces();
            List<Province> provs = new List<Province>();

            for (int i = 0; i < provNames.Count(); i++)
            {
                Province temp = GetNumCityByProvince(provNames[i]);
                provs.Add(temp);
            }//End for

            provs.Sort();

            //Remove Old Data
            lbxProv.ItemsSource = null;
            lbxProv.Items.Clear();
            lblTotals.Content = "";
            for (int i = 0; i < provs.Count(); i++)
            {
                lbxProv.Items.Add(provs[i].ToStringCitiesCount());
            }//End for
            
        }//End of sortCities

        private void GetStats(object sender, SelectionChangedEventArgs e)
        {
            if(lbxProv.Items.Count > 0)
            {
                lblTotals.Content = lbxProv.SelectedItem.ToString();
            }//End if            
        }//End GetSTats EVENT
    }//End of class
}//End of namespace
