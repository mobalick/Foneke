using FoNeke.Web.Models.Actions;

namespace FoNeke.Services
{
    public interface IGeoService
    {
        void GeoCodeAdresses();
        void GeoCodeAdresses(DevicePosition position);
    }
}