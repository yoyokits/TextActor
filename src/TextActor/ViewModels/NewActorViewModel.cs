﻿namespace TextActor.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="NewActorViewModel" />.
    /// </summary>
    public class NewActorViewModel : BaseViewModel
    {
        #region Fields

        private IList<Locale> _locales;

        private string _name;

        private float _pitch;

        private SpeechOptions _speechOptions;

        private string _testText = "Hello, you are using Text Actor voice test";

        private float _volume;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewActorViewModel"/> class.
        /// </summary>
        public NewActorViewModel()
        {
            Name = "No Name";
            Pitch = 0.5f;
            Volume = 0.7f;
            PlayCommand = new Command(OnPlay);
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Gets or sets the Locales.
        /// </summary>
        public IList<Locale> Locales { get => _locales; set => SetProperty(ref _locales, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.SetProperty(ref _name, value); }

        /// <summary>
        /// Gets or sets the Pitch.
        /// </summary>
        public float Pitch { get => _pitch; set => this.SetProperty(ref _pitch, value); }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public ICommand PlayCommand { get; }

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public Command SaveCommand { get; }

        /// <summary>
        /// Gets the SpeechOptions.
        /// </summary>
        public SpeechOptions SpeechOptions
        {
            get
            {
                if (_speechOptions != null)
                {
                    return _speechOptions;
                }

                Locales = TextToSpeechHelper.Locales;
                _speechOptions = new SpeechOptions() { Locale = TextToSpeechHelper.DefaultLocale, Pitch = Pitch, Volume = Volume };
                return _speechOptions;
            }
        }

        /// <summary>
        /// Gets or sets the TestText.
        /// </summary>
        public string TestText { get => _testText; set => this.SetProperty(ref _testText, value); }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume { get => _volume; set => SetProperty(ref _volume, value); }

        /// <summary>
        /// Gets or sets the CancellationTokenSource.
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnCancel.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnCancel(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnPlay(object obj)
        {
            if (string.IsNullOrWhiteSpace(TestText))
            {
                return;
            }

            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            SpeechOptions.Pitch = Pitch;
            SpeechOptions.Volume = Volume;
            await TextToSpeech.SpeakAsync(this.TestText, SpeechOptions, this.CancellationTokenSource.Token);
        }

        /// <summary>
        /// The OnSave.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnSave(object obj)
        {
            var actor = new Actor()
            {
                Name = Name,
                Pitch = Pitch,
                Volume = Volume
            };

            await ActorDataStore.AddItemAsync(actor);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// The ValidateSave.
        /// </summary>
        /// <param name="arg">The arg<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ValidateSave(object arg)
        {
            return Volume > 0f && Pitch > 0f && !string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name) && Name != "No Name";
        }

        #endregion Methods
    }
}