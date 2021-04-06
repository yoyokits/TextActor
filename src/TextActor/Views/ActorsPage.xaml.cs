namespace TextActor.Views
{
    using TextActor.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Defines the <see cref="ActorsPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActorsPage : ContentPage
    {
        #region Fields

        internal ActorsViewModel _viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorsPage"/> class.
        /// </summary>
        public ActorsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ActorsViewModel();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion Methods
    }
}