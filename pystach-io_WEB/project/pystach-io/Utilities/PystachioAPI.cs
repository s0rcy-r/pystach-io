using Nominatim.API.Geocoders;
using Nominatim.API.Models;
using pystach_io.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace pystach_io.Utilities
{
    public class PystachioAPI
    {
        //Function to get coordinates of an address
        private static List<string> _getCoordinates(string address)
        {
            var geocoder = new ForwardGeocoder();
            var coordinates = new List<string>();
            string lat, lon;

            var request = geocoder.Geocode(new ForwardGeocodeRequest { 
                queryString = address
            });

            try
            {
                lat = request.Result[0].Latitude.ToString();
                lon = request.Result[0].Longitude.ToString();
            } catch (IndexOutOfRangeException)
            {
                var Error = new List<string>()
                {
                    null,
                    address
                };

                return Error;
            }

            coordinates.Add(lat);
            coordinates.Add(lon);

            return coordinates;
        }

        //Function to get the distances between two sets of coordinates
        public static double _getDistance(string a, string b, string c, string d)
        {
            var x_a = (Math.PI / 180) * Double.Parse(a);
            var y_a = (Math.PI / 180) * Double.Parse(b);
            var x_b = (Math.PI / 180) * Double.Parse(c);
            var y_b = (Math.PI / 180) * Double.Parse(d);

            return 6378137 * Math.Acos((Math.Sin(x_a) * Math.Sin(x_b)) + (Math.Cos(x_a) * Math.Cos(x_b) * Math.Cos(y_b - y_a)));

        }

        //Function to sort multiple addresses (with Dijkstra algorithm)
        public static List<List<string>> _sortAddress(List<List<string>> coordinatesList)
        {
            var lst_trans = new List<List<object>>();
            var lst_end = new List<List<object>>();
            var lst_final = new List<List<string>>();
            var x = coordinatesList.Count();

            while (x != 0)
            {
                

                for (int i = 0; i < x; i++)
                {
                    var lst_1 = coordinatesList[0];
                    var lst_2 = coordinatesList[i];

                    var y = _getDistance(lst_1[0], lst_1[1], lst_2[0], lst_2[1]);

                    var z = new List<object>()
                    {
                        y,
                        coordinatesList[i][0],
                        coordinatesList[i][1]
                    };

                    lst_trans.Add(z);
                }

                lst_end = lst_trans.OrderBy(list => list[0]).ToList();

                if (lst_end.Count() != 1)
                {
                    lst_final.Add(coordinatesList[0]);
                    coordinatesList.RemoveAt(0);

                    var coord = new List<string>()
                    {
                        lst_end[1][1].ToString(),
                        lst_end[1][2].ToString()
                    };

                    var i = 0;

                    foreach (var j in coordinatesList)
                    {
                        if (j[0] == coord[0])
                        {
                            coordinatesList.RemoveAt(i);
                            break;
                        } else
                        {
                            i++;
                            continue;
                        }
                    }
                    
                    coordinatesList.Add(coord);
                    coordinatesList.Reverse();            
                } else
                {
                    lst_final.Add(coordinatesList[0]);
                    break;
                }

                lst_trans = new List<List<object>>();
                lst_end = new List<List<object>>();

                x--;
                
            }

            return lst_final;
        }

        //Main function
        public static string useIt(List<string> addresses)
        {
            var bg = DateTime.Now;
            var coordinatesList = new List<List<string>>();

            foreach (var address in addresses)
            {
                var result = _getCoordinates(address);

                if (result[0] != null)
                {
                    coordinatesList.Add(result);
                } else
                {
                    var errorObj = new List<string>()
                    {
                        "Error",
                        "AddressError:" + address
                    };

                    foreach (var address_x in addresses)
                    {
                        errorObj.Add(address_x);
                    }

                    var jsonStringError = JsonSerializer.Serialize(errorObj);

                    return jsonStringError;
                }
            }

            var res = _sortAddress(coordinatesList);

            var url_bgn = "https://www.google.com/maps/dir";
            var link_end = "";

            foreach (var ls in res)
            {
                link_end += "/" + ls[0].Replace(",", ".") + "," + ls[1].Replace(",", ".");
            }

            var url = url_bgn + link_end;

            var nd = DateTime.Now;
            var deltaTime = nd - bg;

            var obj = new jsonModel()
            {
                gmapURL = url,
                inputAddresses = addresses,
                sortedAddresses = res,
                time = deltaTime.TotalSeconds.ToString()
            };

            var jsonString = JsonSerializer.Serialize(obj);

            return jsonString;
        }
        
    }
}
