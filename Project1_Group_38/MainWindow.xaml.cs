/*
 * Project 1 - Group 38 Main Window Class
 * David Aaron Braun
 * 2020-02-15
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;
using System.Globalization;

namespace Project1_Group_38
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentItem = 0;
        public Statistics stat;
        public MainWindow()
        {
            InitializeComponent();
            //var returnDictionary = DataModeler.ParseFile("../../Data/Canadcities-JSON.json", "json");  

            Main_GUI.KeyDown += new KeyEventHandler(this.Back_KeyDown);

            Main_GUI.KeyDown += new KeyEventHandler(this.Forward_KeyDown);

            //Set Defaults
            CityStats.IsEnabled = false;
            ProvinceStats.IsEnabled = false;
            ForwardButton.IsEnabled = false;
            BackButton.IsEnabled = false;
            searchBox.IsEnabled = false;
            lbl_Status.Content = "--No Data Loaded! Select a file!--";
        }//End of main

        private void Choose()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.xml)|*.xml|(*.json)|*.json|(*.csv)|*.csv|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                //Delete old data
                if (stat != null)
                {
                    if (stat.CityCatalogue != null)
                    {
                        stat.CityCatalogue.Clear();
                        currentItem = 0;
                    }//End if
                }//End if

                   stat = new Statistics(DataModeler.ParseFile(openFileDialog.FileName, System.IO.Path.GetExtension(openFileDialog.FileName)));

                    //Set First City
                    act_CityName.Content = stat.CityCatalogue.ElementAt(currentItem).Key.ToString();
                    act_Province.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Province;
                    act_Population.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Population.ToString("N",CultureInfo.InvariantCulture);
                    act_Location.Content = stat.CityCatalogue.ElementAt(currentItem).Value.GetLocation().ToString();
                    lbl_Status.Content = "City " + currentItem + " of " + (stat.CityCatalogue.Count-1);
                    //Enable Buttons
                    ForwardButton.IsEnabled = true;
                    BackButton.IsEnabled = true;
                    CityStats.IsEnabled = true;
                    ProvinceStats.IsEnabled = true;
                    searchBox.IsEnabled = true;

                //Populate Search Combo Box
                List<string> searchlist = new List<string>();
                
                for (int i = 0; i < stat.CityCatalogue.Count(); i++)
                {
                    searchlist.Add(stat.CityCatalogue.ElementAt(i).Value.CityName);   
                }//End for
                searchlist.Sort();
                searchBox.ItemsSource = searchlist;
            }//End if               
        }//End of ChooseFile

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            Choose();
        }

        /// <summary>
        /// Moves the city information 1 key forward in the dictionary and displays the new information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Forward(object sender, RoutedEventArgs e)
        {
            GoForward();
        }//End of Forward()

        private void GoForward()
        {
            if (currentItem == (stat.CityCatalogue.Count-1))
            {
                //Got to beginning of lsit if at end
                currentItem = 1;
                GoBack();
            }
            else
            {
                currentItem += 1;

                act_CityName.Content = stat.CityCatalogue.ElementAt(currentItem).Key.ToString();
                act_Province.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Province;
                act_Population.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Population.ToString("N", CultureInfo.InvariantCulture);
                act_Location.Content = stat.CityCatalogue.ElementAt(currentItem).Value.GetLocation().ToString();
                lbl_Status.Content = "City " + currentItem + " of " + (stat.CityCatalogue.Count-1);
            }//End else
        }//End of GoForward()

        /// <summary>
        /// Moves the city info in the dictionary back 1 keya nd displays the information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back(object sender, RoutedEventArgs e)
        {
            GoBack();
        }//End of Back()
        private void GoBack()
        {
            if (currentItem == 0)
            {
                //Got to end of list if at 0
                currentItem = stat.CityCatalogue.Count - 1;
                currentItem += 1;
                GoBack();
            }
            else
            {
                currentItem -= 1;

                act_CityName.Content = stat.CityCatalogue.ElementAt(currentItem).Key.ToString();
                act_Province.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Province;
                act_Population.Content = stat.CityCatalogue.ElementAt(currentItem).Value.Population.ToString("N", CultureInfo.InvariantCulture);
                act_Location.Content = stat.CityCatalogue.ElementAt(currentItem).Value.GetLocation().ToString();
                lbl_Status.Content = "City " + currentItem + " of " + (stat.CityCatalogue.Count-1);
            }//End else
        }//End of Goback()

        private void ProvinceStats_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CityStats_Click(object sender, RoutedEventArgs e)
        {
            //Launch Statistics Frame
            CityStatistcs cs = new CityStatistcs(stat);
            cs.Show();
        }//End of CityStats_Click()

        private void CitySearch()
        {
            if (stat == null)
            {
                //Do Nothing
            }//End if
            else
            {
                CityInfo searched = stat.GetCityByName(searchBox.SelectedItem.ToString());

                act_CityName.Content = searched.CityName;
                act_Province.Content = searched.Province;
                act_Population.Content = searched.Population.ToString("N", CultureInfo.InvariantCulture);
                act_Location.Content = searched.GetLocation().ToString();
                lbl_Status.Content = "Searched City: " + searchBox.Text;
            }//End else
        }//End of CitySearch()
        private void searchBoxChange(object sender, SelectionChangedEventArgs e)
        {
            CitySearch();
        }

        #region Keyboard Events
        private void Back_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (stat == null)
                {
                    //Do Nothing
                }//End if
                else
                {
                    GoBack();
                }//End else

                //Debug.WriteLine("Key Pressed!");
            }
        }

        private void Forward_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (stat == null)
                {
                    //Do Nothing
                }//End if
                else
                {
                    GoForward();
                }//End else
                //Debug.WriteLine("Key Pressed!");
            }
        }

        
        #endregion
    }//End of class
}//End of namespace
