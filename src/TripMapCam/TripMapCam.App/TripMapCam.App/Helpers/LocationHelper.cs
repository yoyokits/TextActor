namespace TripMapCam.App.Helpers
{
    using System.Linq;
    using System.Threading.Tasks;
    using TripMapCam.App.UI.Models;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="LocationHelper" />.
    /// </summary>
    public static class LocationHelper
    {
        #region Methods

        /// <summary>
        /// The GetPhotoLocation.
        /// </summary>
        /// <param name="model">The model<see cref="PhotoModel"/>.</param>
        /// <returns>The <see cref="Task{LocationModel}"/>.</returns>
        public static async Task<LocationModel> GetPhotoLocationAsync(this PhotoModel model)
        {
            if (double.IsNaN(model.Longitude) || double.IsNaN(model.Latitude))
            {
                return null;
            }

            var placemarks = await Geocoding.GetPlacemarksAsync(model.Latitude, model.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemark == null)
            {
                return null;
            }

            var location = new LocationModel
            {
                PhotoId = model.Id,
                AdminArea = placemark.AdminArea,
                CountryCode = placemark.CountryCode,
                CountryName = placemark.CountryName,
                FeatureName = placemark.FeatureName,
                Locality = placemark.Locality,
                SubAdminArea = placemark.SubAdminArea,
                SubLocality = placemark.SubLocality
            };

            return location;
        }

        #endregion
    }
}
