namespace TextActor.Models
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IIdentifiable" />.
    /// </summary>
    public interface IIdentifiable
    {
        #region Properties

        /// <summary>
        /// Gets the Id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        string Name { get; }

        #endregion Properties
    }

    #endregion Interfaces
}