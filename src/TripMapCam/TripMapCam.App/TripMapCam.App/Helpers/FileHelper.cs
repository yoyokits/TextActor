namespace TripMapCam.App.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TripMapCam.App.Common;

    /// <summary>
    /// Defines the <see cref="FileHelper" />.
    /// </summary>
    public static class FileHelper
    {
        #region Methods

        /// <summary>
        /// The GetDefaultPhotoPaths.
        /// </summary>
        /// <returns>The <see cref="IList{string}"/>.</returns>
        public static IList<string> GetDefaultPhotoPaths()
        {
            var folder = AppEnvironment.CameraStorageFolder;
            if (!Directory.Exists(folder))
            {
                return null;
            }

            var files = Directory.GetFiles(folder);
            if (!files.Any())
            {
                return null;
            }

            var sortedFiles = files.ToList();
            sortedFiles.Sort();
            return sortedFiles;
        }

        #endregion
    }
}
