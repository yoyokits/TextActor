namespace TripMapCam.App.UI.Models
{
    using TripMapCam.App.UI.Helpers;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="LocationModel" locat />.
    /// </summary>
    public class LocationModel : NotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the District.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        public Location Location { get; set; }

        #endregion
    }
}
