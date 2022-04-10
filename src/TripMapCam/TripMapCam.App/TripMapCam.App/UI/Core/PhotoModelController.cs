namespace TripMapCam.App.UI.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TripMapCam.App.Dependencies;
    using TripMapCam.App.Helpers;
    using TripMapCam.App.UI.Models;
    using TripMapCam.App.UI.Services;
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
                Latitude = location.latitude,
                Longitude = location.longitude,
                FilePath = filePath
            };
            return model;
        }

        /// <summary>
        /// The GetUnnamedLocationPhotos.
        /// </summary>
        /// <param name="photos">The photos<see cref="IList{PhotoModel}"/>.</param>
        /// <param name="locations">The locations<see cref="HashSet{int}"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="IList{PhotoModel}"/>.</returns>
        internal static IList<PhotoModel> GetUnnamedLocationPhotos(IList<PhotoModel> photos, HashSet<int> locations, CancellationToken token, int count = 10)
        {
            var unnamedPhotos = new List<PhotoModel>(count);
            for (var i = 0; i < photos.Count; i++)
            {
                var photo = photos[i];
                if (token.IsCancellationRequested || unnamedPhotos.Count > count)
                {
                    return unnamedPhotos;
                }

                if (!locations.Contains(photo.Id))
                {
                    unnamedPhotos.Add(photo);
                }
            }

            return unnamedPhotos;
        }

        /// <summary>
        /// The PopulatePhotos.
        /// </summary>
        /// <param name="populatedPhotoPaths">The populatedPhotoPaths<see cref="HashSet{string}"/>.</param>
        /// <param name="allPhotoPaths">The allPhotoPaths<see cref="HashSet{string}"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        /// <param name="count">The count<see cref="int"/> of photo candidates acquired from the folder.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        internal static Task<IList<PhotoModel>> PopulatePhotos(HashSet<string> populatedPhotoPaths, HashSet<string> allPhotoPaths, CancellationToken token, int count = 50)
        {
            return Task.Run<IList<PhotoModel>>(() =>
            {
                var photoModels = new List<PhotoModel>();
                var sw = new Stopwatch();
                sw.Start();
                foreach (var filePath in allPhotoPaths)
                {
                    if (token.IsCancellationRequested)
                    {
                        sw.Stop();
                        return photoModels;
                    }

                    if (!populatedPhotoPaths.Contains(filePath))
                    {
                        var model = GetPhotoData(filePath);
                        if (model != null)
                        {
                            photoModels.Add(model);
                            if (photoModels.Count == count)
                            {
                                break;
                            }
                        }
                    }
                }

                sw.Stop();
                double populatedCount = photoModels.Any() ? photoModels.Count : 1;
                TripMapCam.App.Helpers.Debug.WriteLine($"Populate Photos Elapsed Time: {sw.ElapsedMilliseconds}");
                TripMapCam.App.Helpers.Debug.WriteLine($"Populate Photos Elapsed Time: {sw.ElapsedMilliseconds / populatedCount:0.##}/Photo");
                return photoModels;
            }, token);
        }

        /// <summary>
        /// The UpdatePhotoLocationNames.
        /// </summary>
        /// <param name="dataBase">The dataBase<see cref="TripMapCamDataBase"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        internal static Task UpdatePhotoLocationNames(TripMapCamDataBase dataBase, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                var photos = await dataBase.GetPhotoModelsAsync();
                var acquiredLocations = await dataBase.GetLocationModelsAsync();
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var locationHash = new HashSet<int>(acquiredLocations.Select(location => location.PhotoId));
                var unnamedPhotos = GetUnnamedLocationPhotos(photos, locationHash, token);
                while (unnamedPhotos.Any() && !token.IsCancellationRequested)
                {
                    foreach (var photo in unnamedPhotos)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }

                        var location = await LocationHelper.GetPhotoLocationAsync(photo);
                        await dataBase.SaveLocationModelAsync(location);
                    }

                    unnamedPhotos = GetUnnamedLocationPhotos(photos, locationHash, token);
                }

            }, token);
        }

        #endregion
    }
}
