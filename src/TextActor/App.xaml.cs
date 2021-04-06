namespace TextActor
{
    using TextActor.Helpers;
    using TextActor.Services;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="App" />.
    /// </summary>
    public partial class App : Application
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            DependencyService.Register<ActorDataStore>();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The OnResume.
        /// </summary>
        protected override void OnResume()
        {
        }

        /// <summary>
        /// The OnSleep.
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// The OnStart.
        /// </summary>
        protected override void OnStart()
        {
            TextToSpeechHelper.InitializeAsync();
        }

        #endregion Methods
    }
}