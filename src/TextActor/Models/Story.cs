namespace TextActor.Models
{
    using SQLite;
    using System;

    /// <summary>
    /// Defines the <see cref="Story" />.
    /// </summary>
    public class Story : IIdentifiable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Story"/> class.
        /// </summary>
        public Story()
        {
            Date = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Played Actors in Json.
        /// </summary>
        public string ActorsJson { get; set; }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the DialogsJson.
        /// </summary>
        public string DialogsJson { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"Story Title:{Name};Id:{Id}";
            return message;
        }

        #endregion Methods
    }
}