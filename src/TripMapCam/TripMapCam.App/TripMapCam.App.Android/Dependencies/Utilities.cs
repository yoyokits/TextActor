using TripMapCam.App.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(Utilities))]
namespace TripMapCam.App.Droid.Dependencies
{
    using Android.Media;
    using Android.OS;
    using TripMapCam.App.Dependencies;
    using TripMapCam.App.UI.Models;

    /// <summary>
    /// Defines the <see cref="Utilities" />.
    /// </summary>
    public class Utilities : IUtilities
    {
        #region Methods

        /// <summary>
        /// The GetCameraStorageFolder.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCameraStorageFolder()
        {
            Java.IO.File jFolder;

            if ((int)Build.VERSION.SdkInt >= 29)
            {
                jFolder = new Java.IO.File(Android.App.Application.Context.GetExternalFilesDir(Environment.DirectoryDcim), "Camera");
            }
            else
            {
                jFolder = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim), "Camera");
            }

            return jFolder.AbsolutePath;
        }

        /// <summary>
        /// The GetImageLocationFromExif.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <returns>The <see cref="LocationModel"/>.</returns>
        public (double altitude, double latitude) GetImageLocationFromExif(string filePath)
        {
            var newExif = new ExifInterface(filePath);
            var gpsCoordinate = new float[2];
            return !newExif.GetLatLong(gpsCoordinate) ? (double.NaN, double.NaN) : (gpsCoordinate[0], gpsCoordinate[1]);
        }

        #endregion
    }
}