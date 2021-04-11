namespace TextActor.ViewModels
{
    using System.Threading.Tasks;
    using TextActor.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="BaseDetailViewModel" />.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public abstract class BaseDetailViewModel : BaseViewModel, ISelectable
    {
        #region Fields

        private int _id;

        private bool _isSelected;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDetailViewModel"/> class.
        /// </summary>
        protected BaseDetailViewModel()
        {
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get => _id; set => this.SetProperty(ref _id, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected
        /// Gets or sets the IsSelected..
        /// </summary>
        public bool IsSelected { get => _isSelected; set => this.SetProperty(ref _isSelected, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        protected abstract Task Load(int id);

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private async void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Id):
                    await Load(Id);
                    break;
            }
        }

        #endregion Methods
    }
}