using FoNeke.Web.Models.Actions;
using FoNeke.Web.Repositories.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Services
{


    public class GeoService : IGeoService
    {
        public IDevicePositionRepository DevicePositionRepository { get; set; }

        public GeoService(IDevicePositionRepository _devicePositionRepository)
        {
            DevicePositionRepository = _devicePositionRepository;
        }

        //https://nominatim.openstreetmap.org/reverse?lat=14.453548&lon=-17.050665
        //https://wiki.openstreetmap.org/wiki/Nominatim#Reverse_Geocoding_.2F_Address_lookup
        //https://stackoverflow.com/questions/1961124/reverse-geo-location-using-openstreetmap
        public void GeoCodeAdresses()
        {
            foreach (var position in DevicePositionRepository.GetAll().Where(d=>d.Address == null))
            {
                GeoCodeAdresses(position);
            }
        }


        public void GeoCodeAdresses(DevicePosition position)
        {
            var adress = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={position.Latitude.ToString(CultureInfo.InvariantCulture)},{position.Longitude.ToString(CultureInfo.InvariantCulture)}&key=AIzaSyBto-QEVHk0ognTc3NKSCnhaD1FHYPOIOY".GetJsonFromUrl().FromJson<GoodleGeoCodeResult>();
            position.Address = new Web.Models.Addressing.Address
            {
                AddressLine = adress.results.First().formatted_address
            };

            DevicePositionRepository.SaveOrUpdate(position);
            
        }
    }
}



public class GoodleGeoCodeResult
{
    public Result[] results { get; set; }
    public string status { get; set; }
}

public class Result
{
    public Address_Components[] address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public string place_id { get; set; }
    public string[] types { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
    public Bounds bounds { get; set; }
}

public class Location
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Northeast
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Southwest
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Bounds
{
    public Northeast1 northeast { get; set; }
    public Southwest1 southwest { get; set; }
}

public class Northeast1
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Southwest1
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Address_Components
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public string[] types { get; set; }
}







public class GeoCodeResult
{
    public string Place_id { get; set; }
    public string Osm_type { get; set; }
    public string Osm_id { get; set; }
    public string Lat { get; set; }
    public string Lon { get; set; }
    public string Boundingbox { get; set; }
    public string Text { get; set; }
}

public class Addressparts
{
    public string Residential { get; set; }
    public string Village { get; set; }
    public string State { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
    public string Country_code { get; set; }
}

public class Reversegeocode
{
    public GeoCodeResult Result { get; set; }
    public Addressparts Addressparts { get; set; }
    public string Timestamp { get; set; }
    public string Attribution { get; set; }
    public string Querystring { get; set; }
}