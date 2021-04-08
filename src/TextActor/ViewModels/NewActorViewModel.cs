namespace TextActor.ViewModels
{
    using System.Collections.Generic;
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

        private Locale _selectedLocale;

        private string _testText = "Hello, you are using Text Actor voice test";

        private float _volume;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewActorViewModel"/> class.
        /// </summary>
        public NewActorViewModel()
        {
            Locales = TextToSpeechHelper.Locales;
            Pitch = 0.8f;
            Volume = 0.7f;
            SelectedLocale = TextToSpeechHelper.DefaultLocale;
            TextPlayer = new TextPlayer();
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
        /// Gets or sets the SelectedLocale.
        /// </summary>
        public Locale SelectedLocale { get => _selectedLocale; set => this.SetProperty(ref _selectedLocale, value); }

        /// <summary>
        /// Gets or sets the TestText.
        /// </summary>
        public string TestText { get => _testText; set => this.SetProperty(ref _testText, value); }

        /// <summary>
        /// Gets the TextPlayer.
        /// </summary>
        public TextPlayer TextPlayer { get; }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume { get => _volume; set => SetProperty(ref _volume, value); }

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
            await TextPlayer.Play(TestText, Pitch, Volume, SelectedLocale);
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
                LocaleName = SelectedLocale.Name,
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
            return Volume > 0f && Pitch > 0f && !string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name) && Name != "No Name" && SelectedLocale != null;
        }

        #endregion Methods
    }
}