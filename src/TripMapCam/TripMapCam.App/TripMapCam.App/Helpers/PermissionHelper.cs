namespace TripMapCam.App.Helpers
{
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="PermissionHelper" />.
    /// </summary>
    public static class PermissionHelper
    {
        #region Methods

        /// <summary>
        /// The CheckGetLocationPermission.
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public static async Task<bool> CheckGetLocationPermission()
        {
            var locationStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (locationStatus != PermissionStatus.Granted)
            {
                locationStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            return locationStatus == PermissionStatus.Granted;
        }

        /// <summary>
        /// The CheckWriteReadPermission.
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public static async Task<bool> CheckWriteReadPermission()
        {
            var writeStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (writeStatus != PermissionStatus.Granted)
            {
                writeStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            var readStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (readStatus != PermissionStatus.Granted)
            {
                readStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            return writeStatus == PermissionStatus.Granted && readStatus == PermissionStatus.Granted;
        }

        #endregion
    }
}
