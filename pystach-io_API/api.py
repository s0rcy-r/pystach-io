import googlemaps
import random
import string

from geopy.geocoders import Nominatim
from math import radians, cos, sin, acos


class PystachioAPI:
    def __init__(self, addresses, mode):
        """Initialisation of the class

        Args:
            addresses (list): List of string containing the addresses
            mode (string): Mode of the API (distance or time)
        """

        if mode == "time":
            self.client = googlemaps.Client(key="APIKEYHERE")

        self.addresses = addresses
        self.mode = mode

        self.coordinates = []
        self.url = ""
        self.error = None

    def run(self):
        """Main function of the class

        Returns:
            string: URL to the Google Maps route
        """
        
        mp = self._get_coordinates()

        if mp != None:
            self.error = mp
            return False

        sorted_coordinates = self._sort_addresses()

        url_bgn = "https://www.google.com/maps/dir"
        url_end = ""

        for i, j in sorted_coordinates:
            url_end += "/" + str(i) + "," + str(j)

        self.url = url_bgn + url_end

    def _user_agent_generator(self, stringLength=8):
        """Function to generate user agent for Nominatim.

        Args:
            stringLength (int, optional): Length of the key generated. Defaults to 8.

        Returns:
            string: The key generated.
        """

        letters = string.ascii_lowercase
        return ''.join(random.choice(letters) for i in range(stringLength))

    def _get_coordinates(self):
        """Function to get the coordinates of the addresses.

        Returns:
            string: Return an address if an error occured.
        """

        for address in self.addresses:
            if self.mode == "time":
                try:
                    coordinates = self.client.geocode(address)
                except:
                    return address

                self.coordinates.append([coordinates[0]["geometry"]["location"]["lat"], coordinates[0]["geometry"]["location"]["lng"]])

            else:
                key = self._user_agent_generator(10)

                geolocator = Nominatim(user_agent=key, timeout=3)
                location = geolocator.geocode(address)

                if location == None:
                    return address

                self.coordinates.append([location.latitude, location.longitude])

    def _get_distance(self, a, b, c, d):
        """Function to get the distance between two point (coordinates).

        Args:
            a (float): Coordinate x of the first point
            b (float): Coordinate y of the first point
            c (float): Coordinate x of the second point
            d (float): Coordinate y of the second point

        Returns:
            float: Distance between the two points
        """

        x_a = radians(a)
        x_b = radians(c)
        y_a = radians(b)
        y_b = radians(d)

        distance = 6378137 * acos((sin(x_a) * sin(x_b)) + (cos(x_a) * cos(x_b) * cos(y_b - y_a)))

        return distance
            
    def _get_timetravel(self, a, b, c, d):
        """Function to get the time of travel between two points

        Args:
            a (float): Coordinate x of the first point
            b (float): Coordinate y of the first point
            c (float): Coordinate x of the second point
            d (float): Coordinate y of the second point

        Returns:
            float: Time of travel between the two points
        """

        value = self.client.directions({"lat": a, "lng": b}, {"lat":c, "lng":d})
        ls = value[0]["legs"][0]["duration"]["text"].split(" ")

        if len(ls) == 2:
            return int(ls[0])
        else:
            return int(ls[0]) * 60 + int(ls[2])

    def _sort_addresses(self):
        """Function to sort the addresses in order to get the shortest distance between them and so, the shortest or fatest route.

        Returns:
            list: The list of sorted addresses
        """

        # Initialisation of local variables
        lst_trans = []
        lst_final = []
        i = 1
        j = 0

        # While loop (= to be sure that all of the list is sorted)
        while self.coordinates:
            x = len(self.coordinates)

            # Creation of an array = [[distance][coordinate]]
            for i in range(1, x):
                lst_1 = self.coordinates[0]
                lst_2 = self.coordinates[i]
                if self.mode == "time" and self.premium == True:
                    z = self._get_timetravel(lst_1[0], lst_1[1], lst_2[0], lst_2[1])
                else:
                    z = self._get_distance(lst_1[0], lst_1[1], lst_2[0], lst_2[1])
                lst_trans.append([[round(z)], self.coordinates[i]])

            # Sorting of the array
            lst_end = sorted(lst_trans, key=lambda colonne: colonne[0])

            # Preparation of the next loop (delete the first value and throw at the begginning the most close value)
            if len(lst_end) != 0:
                lst_final.append(self.coordinates[0])
                self.coordinates.pop(0)
                x = lst_end[0][1]
                self.coordinates.remove(lst_end[0][1])
                self.coordinates.append(x)
                self.coordinates.reverse()
            else:
                lst_final.append(self.coordinates[0])
                break

            # Erasing of the local variable for next loop
            lst_end = []
            lst_trans = []

            j += 1

        return lst_final


if __name__ == "__main__":
    mp = PystachioAPI(["Marseille", "Paris", "Lyon", "Lille"], mode="time")
    mp.run()
    print(mp.url)
