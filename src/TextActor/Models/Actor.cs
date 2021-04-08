namespace TextActor.Models
{
    using SQLite;
    using TextActor.Extensions;
    using TextActor.Helpers;

    /// <summary>
    /// Defines the <see cref="Actor" />.
    /// </summary>
    public class Actor : NotifyPropertyChanged
    {
        #region Fields

        private string _localeName;

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
            this.LocaleName = TextToSpeechHelper.DefaultLocaleName;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = IdCounter++;

        /// <summary>
        /// Gets or sets a value indicating whether IsProtected.
        /// </summary>
        public bool IsProtected { get; set; }

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

        /// <summary>
        /// Gets or sets the IdCounter.
        /// </summary>
        private static int IdCounter { get; set; }

        #endregion Properties
    }
}