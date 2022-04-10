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

        /// <summary>
        /// The GetImageLocationFromExif.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <returns>The <see cref="(double latitude, double longitude, double altitude)"/>.</returns>
        (double latitude, double longitude, double altitude) GetImageLocationFromExif(string filePath);

        #endregion
    }

    #endregion
}
