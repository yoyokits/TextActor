namespace TextActor.Models
{
    using SQLite;
    using System;
    using TextActor.Extensions;
    using TextActor.Helpers;

    /// <summary>
    /// Defines the <see cref="Actor" />.
    /// </summary>
    public class Actor : NotifyPropertyChanged, IIdentifiable, IEquatable<Actor>
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
        public int Id { get; set; }

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(Actor other)
        {
            var isEqual = this.Id == other.Id && this.IsProtected == other.IsProtected && this.LocaleName == other.LocaleName && this.Name == other.Name && this.Pitch == other.Pitch && this.Volume == other.Volume;
            return isEqual;
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            var actor = obj as Actor;
            if (null == actor)
            {
                return false;
            }

            return this.Equals(actor);
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 31;
                hashCode = hashCode * 31 ^ this.LocaleName.GetHashCode();
                hashCode = hashCode * 31 ^ this.Name.GetHashCode();
                hashCode = hashCode * 31 ^ this.Pitch.GetHashCode();
                hashCode = hashCode * 31 ^ this.Volume.GetHashCode();
                return hashCode;
            }
        }

        #endregion Methods
    }
}