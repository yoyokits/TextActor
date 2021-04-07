namespace TextActor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using TextActor.Models;
    using TextActor.Services;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="BaseViewModel" />.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _isBusy = false;

        private string _title = string.Empty;

        #endregion Fields

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the ActorDataStore.
        /// </summary>
        public IDataStore<Actor> ActorDataStore => DependencyService.Get<IDataStore<Actor>>();

        /// <summary>
        /// Gets the DataStore.
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        /// <summary>
        /// Gets the DialogDataStore.
        /// </summary>
        public IDataStore<Dialog> DialogDataStore => DependencyService.Get<IDataStore<Dialog>>();

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion Methods
    }
}