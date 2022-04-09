namespace TripMapCam.App.UI.Helpers
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IVisibilityChangedNotifiable" />.
    /// </summary>
    public interface IVisibilityChangedNotifiable
    {
        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        void OnAppearing(object obj);

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        void OnDisappearing(object obj);

        #endregion
    }

    #endregion
}
