namespace TripMapCam.App.UI.Models
{
    using SQLite;
    using System;
    using TripMapCam.App.Helpers;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="PhotoModel" />.
    /// </summary>
    public class PhotoModel : NotifyPropertyChanged
    {
        #region Fields

        private string _city;

        private string _country;

        private DateTime _createdTime;

        private string _filePath;

        private int _impressionCount;

        private Location _location;

        private int _starCount;

        #endregion

        #region Properties

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
        /// Gets or sets the Location.
        /// </summary>
        public Location Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
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
