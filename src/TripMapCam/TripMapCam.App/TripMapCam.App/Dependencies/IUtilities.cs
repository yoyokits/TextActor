namespace TripMapCam.App.Dependencies
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IUtilities" />.
    /// </summary>
    public interface IUtilities
    {
        #region Methods

        /// <summary>
        /// The GetCameraStorageFolder.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string GetCameraStorageFolder();

        #endregion
    }

    #endregion
}
