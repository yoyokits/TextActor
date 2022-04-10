[assembly: Xamarin.Forms.Dependency(typeof(TripMapCam.App.Droid.Dependencies.Utilities))]
namespace TripMapCam.App.Droid.Dependencies
{
    using Android.Media;
    using Android.OS;
    using TripMapCam.App.Dependencies;

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
        /// <returns>The <see cref="(double latitude, double longitude, double altitude)"/>.</returns>
        public (double latitude, double longitude, double altitude) GetImageLocationFromExif(string filePath)
        {
            var exif = new ExifInterface(filePath);
            var altitude = exif.GetAltitude(double.NaN);
            var gpsCoordinate = new float[2];
            return !exif.GetLatLong(gpsCoordinate) ? (double.NaN, double.NaN, altitude) : (gpsCoordinate[0], gpsCoordinate[1], altitude);
        }

        #endregion
    }
}