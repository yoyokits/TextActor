namespace TripMapCam.App.UI.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using TripMapCam.App.Common;
    using TripMapCam.App.UI.Models;

    /// <summary>
    /// Defines the <see cref="PhotoModelController" />.
    /// </summary>
    internal static class PhotoModelController
    {
        #region Methods

        /// <summary>
        /// The PopulatePhotos.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        internal static Task<IList<PhotoModel>> PopulatePhotos()
        {
            return Task.Run<IList<PhotoModel>>(() =>
            {
                var folder = AppEnvironment.CameraStorageFolder;
                if (Directory.Exists(folder))
                {
                    var files = Directory.GetFiles(folder);
                }

                return new List<PhotoModel>();
            });
        }

        #endregion
    }
}
