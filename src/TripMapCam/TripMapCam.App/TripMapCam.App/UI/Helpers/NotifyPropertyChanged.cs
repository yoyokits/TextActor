namespace TripMapCam.App.UI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="NotifyPropertyChanged" />
    /// Standard INotifyPropertyChanged implementation.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the PropertyChangedHandler.
        /// </summary>
        protected PropertyChangedEventHandler PropertyChangedHandler => this.PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/>.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The SetProperty.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="backingStore">The backingStore<see cref="T"/>.</param>
        /// <param name="value">The value<see cref="T"/>.</param>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <param name="onChanged">The onChanged<see cref="Action"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool Set<T>(ref T backingStore, T value,
                [CallerMemberName] string propertyName = "",
                Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
