namespace TextActor.Models
{
    using SQLite;

    /// <summary>
    /// Defines the <see cref="Settings" />.
    /// </summary>
    public class Settings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EditedStoryId.
        /// </summary>
        public int EditedStoryId { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TextPlayerText.
        /// </summary>
        public string TextPlayerText { get; set; }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        public string Version { get; set; }

        #endregion Properties
    }
}