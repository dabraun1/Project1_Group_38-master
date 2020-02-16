using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Project1_Group_38
{
    class DataModeler
    {
        static Dictionary<string, CityInfo> cityDictionary = new Dictionary<string, CityInfo>();
        public delegate void DataModelerDelegate(string filename); 
        public string argh()
        {
            return "";
        }
        public static void parseJSON(string filename)
        {
            //JArray obj = JsonConvert.DeserializeObject<Jarray>(File.ReadAllText(filename));
            var cities = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(filename));
            foreach (JObject city in cities)
            {
                // public CityInfo(string cityID, string cityName, string cityAscii, long population, string province, decimal latitude, decimal longitude)

                string cityID = city.Value<string>("id");
                string cityName = city.Value<string>("city");
                string cityAscii = city.Value<string>("city_ascii");
                decimal latitude = city.Value<decimal>("lat");
                decimal longitude = city.Value<decimal>("lng");
                long population = city.Value<long>("population");
                string province = city.Value<string>("admin_name");
                if (cityDictionary.ContainsKey(cityName))
                    cityName = cityName + ", " + province;
                CityInfo tempCity = new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude);

                
                cityDictionary.Add(cityName, tempCity);
            }

        }
        public static void parseXML(string filename)
        {
            XElement citiesFull = XElement.Load(filename);
            IEnumerable<XElement> cities = citiesFull.Elements();
            foreach (XElement element in cities)
            {
               // string cityName = element.Element("city").Value;

                string cityID = element.Element("id").Value;
                string cityName = element.Element("city").Value;
                string cityAscii = element.Element("city_ascii").Value;
                decimal latitude = Convert.ToDecimal(element.Element("lat").Value);
                decimal longitude = Convert.ToDecimal(element.Element("lng").Value);
                long population = Convert.ToInt64(element.Element("population").Value);
                string province = element.Element("admin_name").Value;


                if (cityDictionary.ContainsKey(cityName))
                    cityName = cityName + ", " + province;
                CityInfo tempCity = new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude);
                cityDictionary.Add(cityName, tempCity);
            }
        }
        public static void parseCSV(string filename)
        {
            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.SetDelimiters(new string[] { "," });
                parser.HasFieldsEnclosedInQuotes = false;
                parser.ReadLine();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    //new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude);
                    //city,city_ascii,lat,lng,country,admin_name,capital,population,id
                    if (cityDictionary.ContainsKey(fields[0]))
                        fields[0] = fields[0] + ", " + fields[5];
                    CityInfo tempCity = new CityInfo(fields[8],fields[0], fields[1], Convert.ToInt64(fields[7]),fields[5], Convert.ToDecimal(fields[2]), Convert.ToDecimal(fields[3]));
                    cityDictionary.Add(fields[0], tempCity);
                }
            }
        }
        public static Dictionary<string, CityInfo> ParseFile(string filename, string filetype)
        {
            DataModelerDelegate parseFileDelegate;

            if (filetype == ".json")
            {
                parseFileDelegate = parseJSON;
                parseFileDelegate(filename);
            }
            else if (filetype == ".xml")
            {
                parseFileDelegate = parseXML;
                parseFileDelegate(filename);
            }
            else if (filetype == ".csv")
            {
                parseFileDelegate = parseCSV;
                parseFileDelegate(filename);
            }
            else
            {
                throw new Exception("Invalid input: " + filetype);
            }
            return cityDictionary;
        }

    }
}
