/*
 * Project 1 - Group 38 Map Class
 * David Aaron Braun
 * 2020-02-16
 */
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
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
using System.Xaml;

namespace Project1_Group_38
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        Statistics mapStat;
        public Map(Statistics s)
        {
            try
            {
                mapStat = s;

                //initMap();
                InitializeComponent();
                

                //Populate Search Combo Box
                List<string> searchlist = new List<string>();

                for (int i = 0; i < mapStat.CityCatalogue.Count(); i++)
                {
                    searchlist.Add(mapStat.CityCatalogue.ElementAt(i).Value.CityName);
                }//End for
                searchlist.Sort();
                cmbCity1.ItemsSource = searchlist;
                cmbCity2.ItemsSource = searchlist;

            }//End try
            catch(Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }//End catch
        }//End of Constructor

        /// <summary>
        /// Calculates the distance between two coords
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="otherLongitude"></param>
        /// <param name="otherLatitude"></param>
        /// <returns></returns>
        public double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)))/1000;//Convert to KM
        }//End of GetDistance()

        private void Compare(object sender, RoutedEventArgs e)
        {
            if (cmbCity1.SelectedItem != null && cmbCity2.SelectedItem != null)
            {
                CityInfo city1 = mapStat.GetCityByName(cmbCity1.SelectedValue.ToString());

                CityInfo city2 = mapStat.GetCityByName(cmbCity2.SelectedValue.ToString());

                lblDistance.Content = GetDistance(
                    Convert.ToDouble(city1.Latitude),
                    Convert.ToDouble(city1.Longitude),
                    Convert.ToDouble(city2.Latitude),
                    Convert.ToDouble(city2.Longitude)
                    ).ToString("N", CultureInfo.InvariantCulture) + "KM Apx.";
            }//End if
        }//End of Compare EVENT

        /*
        private async void initMap()
        {
            if (mapControl != null)
            {
                BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
                var cityCenter = new Geopoint(cityPosition);

                // Set the map location.
                await mapControl.TrySetViewAsync(cityCenter, 12);
            }//End if
        }//End of initMap

        private async void load(object sender, RoutedEventArgs e)
        {
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            var cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            await(sender as MapControl).TrySetViewAsync(cityCenter, 12);
        }//End of laod EVENT

        */
        private async void goTo(object sender, SelectionChangedEventArgs e)
        {
            CityInfo city = mapStat.GetCityByName(cmbCity1.SelectedValue.ToString());

            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = Convert.ToDouble(city.Latitude), Longitude = Convert.ToDouble(city.Longitude) };

            var cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            //await (sender as MapControl).TrySetViewAsync(cityCenter, 12);
        }//End of goTo EVENT
    }//End of class
}//End of namespace
