using TripMapCam.App.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(Utilities))]
namespace TripMapCam.App.Droid.Dependencies
{
    using Android.OS;
    using TripMapCam.App.Dependencies;

    /// <summary>
    /// Defines the <see cref="Utilities" />.
    /// </summary>
    public class Utilities : IUtilities
    {
        #region Methods

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

        #endregion
    }
}
