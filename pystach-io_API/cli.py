import argparse
import webbrowser

from api import PystachioAPI


def main():
    parser = argparse.ArgumentParser(description='PyS3 Tower CLI')
    parser.add_argument("-ak", "--apikey", help="Google API key")
    parser.add_argument("-m", "--mode", help="Mode of the API (distance or time)")
    parser.add_argument("-a", "--addresses", help="Addresses to compare")

    args = parser.parse_args()

    res = PystachioAPI(args.addresses.split(","), args.mode, args.apikey)
    res.run()
    webbrowser.open(res.url)


main()
