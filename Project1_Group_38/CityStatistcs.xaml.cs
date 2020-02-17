/*
 * Project 1 - Group 38 City Statistcs Class
 * David Aaron Braun
 * 2020-02-15
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
    /// Interaction logic for CityStatistcs.xaml
    /// </summary>
    public partial class CityStatistcs : Window
    {
        Statistics citystats;
        public CityStatistcs(Statistics s)
        {
            citystats = s;
            InitializeComponent();

            //Handle Events for List Boxes
            lbx_Province1.SelectionChanged += new SelectionChangedEventHandler(lbxProvince1_SelectedIndexChanged);

            lbx_Province2.SelectionChanged += new SelectionChangedEventHandler(lbxProvince2_SelectedIndexChanged);

            //Populate List Boxes
            for (int i = 0; i < citystats.CityCatalogue.Count(); i++)
            {
                string prov = citystats.CityCatalogue.ElementAt(i).Value.Province;
                if (lbx_Province1.Items.Contains(prov))
                {
                    //Don't add if exists
                }//End if
                else
                {
                    lbx_Province1.Items.Add(prov);
                }//End else                
            }//End for

            for (int i = 0; i < citystats.CityCatalogue.Count(); i++)
            {
                string prov = citystats.CityCatalogue.ElementAt(i).Value.Province;
                if (lbx_Province2.Items.Contains(prov))
                {
                    //Don't add if exists
                }//End if
                else
                {
                    lbx_Province2.Items.Add(prov);
                }//End else    
            }
        }//End of Constructor

        private void lbxProvince1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //1st Province Selected
            if (lbx_Province1.SelectedItems.Count > 0)
            {
                //Get City Stats
                CityInfo bigcity = citystats.GetLargestPopulationCityByProvince(lbx_Province1.SelectedItem.ToString());

                CityInfo smallcity =                    citystats.GetSmallestPopulationCityByProvince(lbx_Province1.SelectedItem.ToString());

                lbl_CityPop.Content = "Biggest: "+ bigcity.CityName + "(" + bigcity.Population.ToString("N", CultureInfo.InvariantCulture)+")" + "\n"+
                "Smallest: " + smallcity.CityName + "(" + smallcity.Population.ToString("N",CultureInfo.InvariantCulture) + ")";
            }//End if

            //Clean cities list on new selection
            if(provinceCities.Items.Count > 0)
            {
                provinceCities.Items.Clear();
                lblCityCount.Content = "";
            }//End if
        }//End of ListBox Event

        private void lbxProvince2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //1st Province Selected
            if (lbx_Province2.SelectedItems.Count > 0)
            {
                //Get City Stats
                CityInfo bigcity = citystats.GetLargestPopulationCityByProvince(lbx_Province2.SelectedItem.ToString());

                CityInfo smallcity = citystats.GetSmallestPopulationCityByProvince(lbx_Province2.SelectedItem.ToString());

                lbl_CityPop.Content = "Biggest: " + bigcity.CityName + "(" + bigcity.Population.ToString("N", CultureInfo.InvariantCulture) + ")" + "\n" +
                "Smallest: " + smallcity.CityName + "(" + smallcity.Population.ToString("N", CultureInfo.InvariantCulture) + ")";
            }//End if
        }//End of ListBox Event

        private void Compare(object sender, RoutedEventArgs e)
        {
            if(lbx_Province1.SelectedItems.Count > 0 &&
                lbx_Province2.SelectedItems.Count > 0)
            {
                string p1 = lbx_Province1.SelectedItem.ToString();
                string p2 = lbx_Province2.SelectedItem.ToString();

                //Get City Stats
                CityInfo bigcity1 = citystats.GetLargestPopulationCityByProvince(lbx_Province1.SelectedItem.ToString());

                CityInfo smallcity1 = citystats.GetSmallestPopulationCityByProvince(lbx_Province1.SelectedItem.ToString());

                CityInfo bigcity2 = citystats.GetLargestPopulationCityByProvince(lbx_Province2.SelectedItem.ToString());

                CityInfo smallcity2 = citystats.GetSmallestPopulationCityByProvince(lbx_Province2.SelectedItem.ToString());

                lbl_CityPop.Content = p1+ "'s Biggest: " + bigcity1.CityName + "(" + bigcity1.Population.ToString("N", CultureInfo.InvariantCulture) + ")" + "\n" +
                "Smallest: " + smallcity1.CityName + "(" + smallcity1.Population.ToString("N", CultureInfo.InvariantCulture) + ") "
                + "\n"+p2 + "'s Biggest: " + bigcity2.CityName + "(" + bigcity2.Population.ToString("N", CultureInfo.InvariantCulture) + ")" + "\n" +
                "Smallest: " + smallcity2.CityName + "(" + smallcity2.Population.ToString("N", CultureInfo.InvariantCulture) + ")";

                lbl_CityPop.ToolTip = p1 + "'s Biggest: " + bigcity1.CityName + "(" + bigcity1.Population.ToString("N", CultureInfo.InvariantCulture) + ")" + "\n" +
                "Smallest: " + smallcity1.CityName + "(" + smallcity1.Population.ToString("N", CultureInfo.InvariantCulture) + ") "
                + "\n" + p2 + "'s Biggest: " + bigcity2.CityName + "(" + bigcity2.Population.ToString("N", CultureInfo.InvariantCulture) + ")" + "\n" +
                "Smallest: " + smallcity2.CityName + "(" + smallcity2.Population.ToString("N", CultureInfo.InvariantCulture) + ")";
            }//End if
        }//End of Compare

        private void listCities(object sender, RoutedEventArgs e)
        {
            int citycount = 0;

            if (lbx_Province1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < citystats.CityCatalogue.Count(); i++)
                {
                    if (citystats.CityCatalogue.ElementAt(i).Value.Province == lbx_Province1.SelectedItem.ToString())
                    {
                        citycount += 1;
                        provinceCities.Items.Add(citystats.CityCatalogue.ElementAt(i).Value.CityName);
                    }//End if
                }//End for
                lblCityCount.Content = "# of Cities: " + citycount;
                provinceCities.ToolTip = "Cites of "+ lbx_Province1.SelectedItem.ToString();
            }//End if
        }//End of ListCities EVENT

        private void sumPop(object sender, RoutedEventArgs e)
        {
            long population = 0;

            if (lbx_Province1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < citystats.CityCatalogue.Count(); i++)
                {
                    if (citystats.CityCatalogue.ElementAt(i).Value.Province == lbx_Province1.SelectedItem.ToString())
                    {
                        population += citystats.CityCatalogue.ElementAt(i).Value.Population;
                    }//End if
                }//End for
                lblPopulation.Content = "Population of \n" + lbx_Province1.SelectedItem.ToString() + ": \n" + population.ToString("N", CultureInfo.InvariantCulture);
                lblPopulation.ToolTip = "Population of \n" + lbx_Province1.SelectedItem.ToString() + ": " + population.ToString("N", CultureInfo.InvariantCulture);
            }//End if
        }//End of sumPop EVENT

        private void seeMap(object sender, RoutedEventArgs e)
        {
            //Launch Map Frame
            Map mp = new Map(citystats);
            mp.Show();
        }//End of seeMap EVENT
    }//End of class
}//End of namespace
