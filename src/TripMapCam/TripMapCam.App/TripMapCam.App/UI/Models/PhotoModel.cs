namespace TripMapCam.App.UI.Models
{
    using SQLite;
    using System;
    using TripMapCam.App.Helpers;

    /// <summary>
    /// Defines the <see cref="PhotoModel" />.
    /// </summary>
    public class PhotoModel : NotifyPropertyChanged
    {
        #region Fields

        private double _altitude;

        private string _city;

        private string _country;

        private DateTime _createdTime;

        private string _filePath;

        private int _impressionCount;

        private double _latitude;

        private int _locationId;

        private int _starCount;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Altitude.
        /// </summary>
        public double Altitude
        {
            get { return _altitude; }
            set
            {
                if (_altitude != value)
                {
                    _altitude = value;
                    OnPropertyChanged(nameof(Altitude));
                }
            }
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the CreatedTime.
        /// </summary>
        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set
            {
                if (_createdTime != value)
                {
                    _createdTime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the FilePath.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ImpressionCount.
        /// </summary>
        public int ImpressionCount
        {
            get { return _impressionCount; }
            set
            {
                if (_impressionCount != value)
                {
                    _impressionCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Latitude.
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    OnPropertyChanged(nameof(Latitude));
                }
            }
        }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                if (_locationId != value)
                {
                    _locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the StarCount.
        /// </summary>
        public int StarCount
        {
            get { return _starCount; }
            set
            {
                if (_starCount != value)
                {
                    _starCount = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}