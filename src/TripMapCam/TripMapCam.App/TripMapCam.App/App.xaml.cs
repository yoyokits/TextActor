namespace TripMapCam.App
{
    using System;
    using System.IO;
    using TripMapCam.App.Common;
    using TripMapCam.App.Dependencies;
    using TripMapCam.App.Helpers;
    using TripMapCam.App.UI.Services;
    using TripMapCam.App.UI.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="App" />.
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private static TripMapCamDataBase _database;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new DashboardPage();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton Database.
        /// </summary>
        public static TripMapCamDataBase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TripMapCamDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TripMapCam.db3"));
                }

                return _database;
            }
        }

        #endregion

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
        protected override async void OnStart()
        {
            await PermissionHelper.CheckWriteReadPermission();
            AppEnvironment.CameraStorageFolder = DependencyService.Get<IUtilities>().GetCameraStorageFolder();
        }

        #endregion
    }
}
