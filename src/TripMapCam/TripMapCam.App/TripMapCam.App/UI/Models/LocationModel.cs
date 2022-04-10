namespace TripMapCam.App.UI.Models
{
    using SQLite;
    using System;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="LocationModel" locat />.
    /// </summary>
    public class LocationModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Accuracy.
        /// </summary>
        public double Accuracy { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the AdminArea.
        /// </summary>
        public string AdminArea { get; internal set; }

        /// <summary>
        /// Gets or sets the Altitude.
        /// </summary>
        public double Altitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the AltitudeReferenceSystem.
        /// </summary>
        public AltitudeReferenceSystem AltitudeReferenceSystem { get; set; }

        /// <summary>
        /// Gets or sets the CountryCode.
        /// </summary>
        public string CountryCode { get; internal set; }

        /// <summary>
        /// Gets or sets the CountryName.
        /// </summary>
        public string CountryName { get; internal set; }

        /// <summary>
        /// Gets or sets the FeatureName.
        /// </summary>
        public string FeatureName { get; internal set; }

        /// <summary>
        /// Gets or sets the Latitude.
        /// </summary>
        public double Latitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the Locality.
        /// </summary>
        public string Locality { get; internal set; }

        /// <summary>
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the Photo Id..
        /// </summary>
        [Unique, Indexed]
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the Speed.
        /// </summary>
        public double Speed { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the SubAdminArea.
        /// </summary>
        public string SubAdminArea { get; internal set; }

        /// <summary>
        /// Gets or sets the SubLocality.
        /// </summary>
        public string SubLocality { get; internal set; }

        /// <summary>
        /// Gets or sets the Timestamp.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the VerticalAccuracy.
        /// </summary>
        public double VerticalAccuracy { get; set; } = double.NaN;

        #endregion
    }
}
