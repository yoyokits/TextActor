namespace TripMapCam.App.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="Debug" />.
    /// </summary>
    public static class Debug
    {
        #region Methods

        /// <summary>
        /// The WriteException.
        /// </summary>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        /// <param name="callerName">The callerName<see cref="string"/>.</param>
        public static void WriteException(this Exception e, [CallerMemberName] string callerName = "")
        {
#if DEBUG
            WriteLine("Error Exception in: " + $"{callerName}");
            WriteLine($"Exception:{e.Message}");

            if (e.InnerException != null)
            {
                WriteLine($"Inner Exception:{e.InnerException.Message}");
            }

            WriteLine("Stack Trace:");
            WriteLine($"{e.StackTrace}");
#endif
        }

        /// <summary>
        /// The WriteLine.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void WriteLine(this string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        #endregion Methods
    }
}
