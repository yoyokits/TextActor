namespace TextActor.ViewModels
{
    using System.Threading;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="TextPlayerViewModel" />.
    /// </summary>
    public class TextPlayerViewModel : BaseViewModel
    {
        #region Fields

        private bool _isPlaying;

        private string _playButtonImage = "media_play.png";

        private Actor _selectedActor;

        private SpeechOptions _speechOptions;

        private string _text;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPlayerViewModel"/> class.
        /// </summary>
        public TextPlayerViewModel()
        {
            this.Title = "Text Player";
            this.Text = string.Empty;
            this.ClearCommand = new Command(this.OnClear);
            this.PlayCommand = new Command(this.OnPlay);
            this.PasteAndPlayCommand = new Command(this.OnPasteAndPlayAsync);
            this.SelectedActor = new Actor { Name = "James Bond", Pitch = 0.7f, Volume = 0.7f };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ClearCommand.
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Gets the PasteAndPlayCommand.
        /// </summary>
        public ICommand PasteAndPlayCommand { get; }

        /// <summary>
        /// Gets or sets the PlayButtonImage.
        /// </summary>
        public string PlayButtonImage { get => _playButtonImage; set => this.SetProperty(ref _playButtonImage, value); }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public ICommand PlayCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedActor.
        /// </summary>
        public Actor SelectedActor { get => _selectedActor; set => _selectedActor = value; }

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

                _speechOptions = new SpeechOptions() { Locale = TextToSpeechHelper.DefaultLocale, Pitch = this.SelectedActor.Pitch, Volume = this.SelectedActor.Volume };
                return _speechOptions;
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text { get => _text; set => this.SetProperty(ref _text, value); }

        /// <summary>
        /// Gets or sets the CancellationTokenSource.
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsPlaying.
        /// </summary>
        private bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                if (_isPlaying == value)
                {
                    return;
                }

                _isPlaying = value;
                this.PlayButtonImage = this.IsPlaying ? "media_stop.png" : "media_play.png";
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The PlayAsync.
        /// </summary>
        internal async void PlayAsync()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }

            this.CancellationTokenSource?.Cancel();
            this.CancellationTokenSource = new CancellationTokenSource();
            this.IsPlaying = true;
            await TextToSpeech.SpeakAsync(this.Text, this.SpeechOptions, this.CancellationTokenSource.Token);
            this.IsPlaying = false;
        }

        /// <summary>
        /// The OnClear.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClear(object obj)
        {
            this.Text = string.Empty;
        }

        /// <summary>
        /// The OnPasteAndPlayAsync.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnPasteAndPlayAsync(object obj)
        {
            this.StopPlaying();
            this.Text = await Clipboard.GetTextAsync();
            this.PlayAsync();
        }

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPlay(object obj)
        {
            if (this.IsPlaying)
            {
                this.StopPlaying();
                return;
            }

            this.PlayAsync();
        }

        /// <summary>
        /// The StopPlaying.
        /// </summary>
        private void StopPlaying()
        {
            if (!this.IsPlaying)
            {
                return;
            }

            this.IsPlaying = false;
            if (this.CancellationTokenSource != null && !this.CancellationTokenSource.IsCancellationRequested)
            {
                this.CancellationTokenSource.Cancel();
            }
        }

        #endregion Methods
    }
}