# Pystach.io :earth_africa: | by s0rcy_r『魔女』

![GitHub repo size](https://img.shields.io/github/repo-size/s0rcy-r/pystach-io?style=for-the-badge)
![Lines of code](https://img.shields.io/tokei/lines/github/s0rcy-r/pystach-io?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/s0rcy-r/pystach-io?style=for-the-badge)

## :warning: WORK IN PROGRESS :warning:

## Description

Pystach.io :earth_africa: is the direct evolution of [MaPyto](https://www.github.com/s0rcy-r/mapyto), an open source project that can "optimize" Google Maps Routes without any addresses limitation. The core API is based on the [Dijkstra algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm), that is used to find the shortest path between two points in graph *(in this case, we are looking for the shortest/fatest path between a lot of points on a map)*.

The API is written in Python and use the [Google Maps API](https://developers.google.com/maps/documentation/javascript/reference) to get the data and it's designed to be used with AWS Lambda. As you may know, Google Maps API is not free, so you can use the API with Nominatim (free) to get the data *(but, you can't use Nominatim to find the fatest route, so the result can be different and less accurate)*.

This project is still in development, so the API is not stable yet. It comes with a .Net Core 3.1 project *(with an API translated in C#)* to demonstrate the usage of the API. Here under you will find how to deploy and test it. You can also use the API with a CLI (command line interface) to test it, available in this repository.

## Usage

### CLI Usage

First, install the requirements :

    pip install -r requirements.txt

Then, you can use the CLI to test the API :

    cli.py --help
    cli.py --apikey=<your-api-key> --mode=<mode> --addresses=<addresses>

Example :

    cli.py --apikey="GOOGLEMAPSAPIKEY" --mode="time/distance" --addresses="X,Y,Z"

### .Net Core 3.1 Usage

To test the API, you can use the .Net Core 3.1 project. You have two choice : 
- the first one is to use Visual Studio and try like that,
- the second one is to deploy it using Docker.

For the second option, you'll have to publish the project with the following command :

    dotnet publish -c Release -r linux-x64

Then, you can deploy the project with the following command :

    docker build -t pystachio .

Then, you can run the project with the following command :

    docker run -it -p 5000:5000 pystachio

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request

## Social

![Twitter Follow](https://img.shields.io/twitter/follow/s0rcy_r?style=social)
![GitHub followers](https://img.shields.io/github/followers/s0rcy-r?label=Follow%20me&style=social)
![GitHub Repo stars](https://img.shields.io/github/stars/s0rcy-r/pystach-io?style=social)