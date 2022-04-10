namespace TripMapCam.App.UI.ViewModels
{
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

        #endregion

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public async void OnAppearing(object obj)
        {
            await this.LoadDataBase();
        }

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnDisappearing(object obj)
        {
        }

        /// <summary>
        /// The LoadDataBase.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        internal async Task LoadDataBase()
        {
            var dataBase = App.Database;
            var photoModels = await dataBase.GetPhotoModelsAsync();
            //if (!photomodels.any())
            {
                await PhotoModelController.PopulatePhotos();
            }
        }

        #endregion
    }
}
