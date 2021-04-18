namespace TextActor
{
    using System;
    using System.IO;
    using TextActor.Helpers;
    using TextActor.Services;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="App" />.
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private static TextActorDataBase _database;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        #endregion Constructors

        #region Properties

        // Create the database connection as a singleton.
        /// <summary>
        /// Gets the Database.
        /// </summary>
        public static TextActorDataBase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TextActorDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TextActor.db3"));
                }

                return _database;
            }
        }

        #endregion Properties

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