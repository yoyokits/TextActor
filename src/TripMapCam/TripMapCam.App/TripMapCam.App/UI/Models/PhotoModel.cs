namespace TripMapCam.App.UI.Models
{
    using SQLite;
    using System;
    using TripMapCam.App.Helpers;

    /// <summary>
    /// Defines the <see cref="PhotoModel" />.
    /// </summary>
    public class PhotoModel : NotifyPropertyChanged, IEquatable<PhotoModel>
    {
        #region Fields

        private double _altitude = double.NaN;

        private string _city;

        private string _country;

        private string _filePath;

        private int _impressionCount;

        private double _latitude = double.NaN;

        private int _locationId;

        private double _longitude = double.NaN;

        private int _starCount;

        private string _thumbPath;

        private DateTime _timeCreated;

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
                    OnPropertyChanged();
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
        /// Gets or sets the FilePath.
        /// </summary>
        [Indexed, Unique]
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
                    OnPropertyChanged();
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
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
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

        /// <summary>
        /// Gets or sets the ThumbPath.
        /// </summary>
        public string ThumbPath
        {
            get { return _thumbPath; }
            set
            {
                if (_thumbPath != value)
                {
                    _thumbPath = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the TimeCreated.
        /// </summary>
        public DateTime TimeCreated
        {
            get { return _timeCreated; }
            set
            {
                if (_timeCreated != value)
                {
                    _timeCreated = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PhotoModel model))
            {
                return false;
            }

            return this.Equals(model);
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="PhotoModel"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(PhotoModel other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var isEqual = this.FilePath == other.FilePath;
            return isEqual;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            return this.FilePath.GetHashCode();
        }

        #endregion
    }
}
