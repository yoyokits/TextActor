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
        /// Gets or sets the Altitude.
        /// </summary>
        public double Altitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the AltitudeReferenceSystem.
        /// </summary>
        public AltitudeReferenceSystem AltitudeReferenceSystem { get; set; }

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
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Latitude.
        /// </summary>
        public double Latitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the Speed.
        /// </summary>
        public double Speed { get; set; } = double.NaN;

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
