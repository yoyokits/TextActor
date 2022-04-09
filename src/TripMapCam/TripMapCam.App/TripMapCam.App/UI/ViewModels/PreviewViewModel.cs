namespace TripMapCam.App.UI.ViewModels
{
    using System.Collections.Generic;
    using TripMapCam.App.Helpers;
    using TripMapCam.App.UI.Models;

    /// <summary>
    /// Defines the <see cref="PreviewViewModel" />.
    /// </summary>
    public class PreviewViewModel : NotifyPropertyChanged
    {
        #region Fields

        private IList<PhotoModel> _photoModels = new List<PhotoModel>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewViewModel"/> class.
        /// </summary>
        public PreviewViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PhotoModels.
        /// </summary>
        public IList<PhotoModel> PhotoModels
        {
            get { return _photoModels; }
            set
            {
                if (_photoModels != value)
                {
                    _photoModels = value;
                    OnPropertyChanged(nameof(PhotoModels));
                }
            }
        }

        #endregion
    }
}
