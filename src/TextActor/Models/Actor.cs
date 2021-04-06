namespace TextActor.Models
{
    using System;
    using TextActor.Extensions;
    using TextActor.Helpers;

    /// <summary>
    /// Defines the <see cref="Actor" />.
    /// </summary>
    public class Actor : NotifyPropertyChanged
    {
        #region Fields

        private string _id;

        private string _localeName = "English(United States)";

        private string _name;

        private float _pitch;

        private float _volume;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        public Actor()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get => _id; set => this.Set(this.PropertyChangedHandler, ref _id, value); }

        /// <summary>
        /// Gets or sets the LocaleName.
        /// </summary>
        public string LocaleName { get => _localeName; set => this.Set(this.PropertyChangedHandler, ref _localeName, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.Set(this.PropertyChangedHandler, ref _name, value); }

        /// <summary>
        /// Gets or sets the Pitch.
        /// </summary>
        public float Pitch { get => _pitch; set => this.Set(this.PropertyChangedHandler, ref _pitch, value); }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume { get => _volume; set => this.Set(this.PropertyChangedHandler, ref _volume, value); }

        #endregion Properties
    }
}