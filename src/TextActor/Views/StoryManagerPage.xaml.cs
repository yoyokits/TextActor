namespace TextActor.Views
{
    using TextActor.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Defines the <see cref="StoryManagerPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoryManagerPage : ContentPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryManagerPage"/> class.
        /// </summary>
        public StoryManagerPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new StoryManagerViewModel();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ViewModel.
        /// </summary>
        public StoryManagerViewModel ViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        #endregion Methods
    }
}