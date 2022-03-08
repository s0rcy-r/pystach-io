# Pystach.io :earth_africa: | by s0rcy_r『魔女』

![GitHub repo size](https://img.shields.io/github/repo-size/s0rcy-r/pystach-io?style=for-the-badge)
![Lines of code](https://img.shields.io/tokei/lines/github/s0rcy-r/pystach-io?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/s0rcy-r/pystach-io?style=for-the-badge)

## :warning: WORK IN PROGRESS :warning:

## Description

Pystach.io :earth_africa: is the direct evolution of [MaPyto](https://www.github.com/s0rcy-r/mapyto), an open source project that can "optimize" Google Maps Routes without any addresses limitation. The core API is based on the [Dijkstra algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm), that is used to find the shortest path between two points in graph *(in this case, we are looking for the shortest/fatest path between a lot of points on a map)*.

The API is written in Python and use the [Google Maps API](https://developers.google.com/maps/documentation/javascript/reference) to get the data and it's designed to be used with AWS Lambda. As you may know, Google Maps API is not free, so you can use the API with Nominatim (free) to get the data *(but, you can't use Nominatim to find the fatest route, so the result can be different and less accurate)*.