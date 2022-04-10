namespace TripMapCam.App.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TripMapCam.App.Helpers;
    using TripMapCam.App.UI.Core;
    using TripMapCam.App.UI.Helpers;

    /// <summary>
    /// Defines the <see cref="DashboardViewModel" />.
    /// </summary>
    public class DashboardViewModel : NotifyPropertyChanged, IVisibilityChangedNotifiable
    {
        #region Properties

        /// <summary>
        /// Gets the PreviewViewModel.
        /// </summary>
        public PreviewViewModel PreviewViewModel { get; } = new PreviewViewModel();

        /// <summary>
        /// Gets or sets the LoadPhotosCancellationTokenSource.
        /// </summary>
        private CancellationTokenSource LoadPhotosCancellationTokenSource { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public async void OnAppearing(object obj)
        {
            await this.LoadPhotos();
        }

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnDisappearing(object obj)
        {
        }

        /// <summary>
        /// The LoadPhotos.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        internal async Task LoadPhotos()
        {
            // Load populated photos from the data base.
            var dataBase = App.Database;
            var populatedPhotos = await dataBase.GetPhotoModelsAsync();
            var populatedPhotoPaths = new HashSet<string>(populatedPhotos.Select(photo => photo.FilePath));
            var photoPaths = FileHelper.GetDefaultPhotoPaths();
            if (photoPaths == null || !photoPaths.Any())
            {
                return;
            }

            this.LoadPhotosCancellationTokenSource?.Cancel();
            this.LoadPhotosCancellationTokenSource = new CancellationTokenSource();
            var token = this.LoadPhotosCancellationTokenSource.Token;
            await Task.Run(async () =>
            {
                // Try to get the unpopulated photos.
                var allPhotoPaths = new HashSet<string>(photoPaths);
                var photos = await PhotoModelController.PopulatePhotos(populatedPhotoPaths, allPhotoPaths, token);
                while (photos.Any())
                {
                    dataBase.SavePhotoModelsAsync(photos, token);
                    photos = await PhotoModelController.PopulatePhotos(populatedPhotoPaths, allPhotoPaths, token);
                }
            }, token);
        }

        #endregion
    }
}
