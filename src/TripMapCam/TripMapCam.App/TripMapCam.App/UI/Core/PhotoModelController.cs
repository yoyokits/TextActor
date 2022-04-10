namespace TripMapCam.App.UI.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using TripMapCam.App.Common;
    using TripMapCam.App.Dependencies;
    using TripMapCam.App.UI.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="PhotoModelController". />.
    /// </summary>
    internal static class PhotoModelController
    {
        #region Methods

        /// <summary>
        /// The GetPhotoData.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <returns>The <see cref="PhotoModel"/>.</returns>
        internal static PhotoModel GetPhotoData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                TripMapCam.App.Helpers.Debug.WriteLine($"Error: File {filePath} doesn't exist");
                return null;
            }

            var location = DependencyService.Get<IUtilities>().GetImageLocationFromExif(filePath);
            if (double.IsNaN(location.altitude) || double.IsNaN(location.latitude))
            {
                return null;
            }

            var model = new PhotoModel
            {
                Altitude = location.altitude,
                Latitude = location.latitude
            };
            return model;
        }

        /// <summary>
        /// The PopulatePhotos.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        internal static Task<IList<PhotoModel>> PopulatePhotos()
        {
            return Task.Run<IList<PhotoModel>>(() =>
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
                var photoModels = new List<PhotoModel>();
                var sw = new Stopwatch();
                sw.Start();
                foreach (var filePath in sortedFiles)
                {
                    var model = GetPhotoData(filePath);
                    if (model != null)
                    {
                        photoModels.Add(model);
                    }

                }

                sw.Stop();
                TripMapCam.App.Helpers.Debug.WriteLine($"Populate Photos Elapsed Time: {sw.ElapsedMilliseconds}");
                TripMapCam.App.Helpers.Debug.WriteLine($"Populate Photos Elapsed Time: {sw.ElapsedMilliseconds / (double)sortedFiles.Count:0.##}/Photo");
                return photoModels;
            });
        }

        #endregion
    }
}
