namespace TextActor.Helpers
{
    using System.Threading;
    using System.Threading.Tasks;
    using TextActor.Extensions;
    using TextActor.Models;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="TextPlayer" />.
    /// </summary>
    public class TextPlayer : NotifyPropertyChanged
    {
        #region Fields

        private bool _isPlaying;

        private string _playButtonImage = "media_play.png";

        private SpeechOptions _speechOptions;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the DefaultTestText.
        /// </summary>
        public static string DefaultTestText { get; } = "Hello, you are using Text Actor voice test";

        /// <summary>
        /// Gets the PlayButtonImage.
        /// </summary>
        public string PlayButtonImage { get => _playButtonImage; private set => this.Set(this.PropertyChangedHandler, ref _playButtonImage, value); }

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

                _speechOptions = new SpeechOptions() { Locale = TextToSpeechHelper.DefaultLocale, Pitch = 0.8f, Volume = 0.8f };
                return _speechOptions;
            }
        }

        /// <summary>
        /// Gets or sets the CancellationTokenSource.
        /// </summary>
        private static CancellationTokenSource CancellationTokenSource { get; set; }

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
        /// The Play.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        internal async Task Play(string text, Actor actor)
        {
            var local = TextToSpeechHelper.GetLocale(actor.LocaleName);
            await this.Play(text, actor.Pitch, actor.Volume, local);
        }

        /// <summary>
        /// The Play.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <param name="pitch">The pitch<see cref="float"/>.</param>
        /// <param name="volume">The volume<see cref="float"/>.</param>
        /// <param name="locale">The locale<see cref="Locale"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        internal async Task Play(string text, float pitch, float volume, Locale locale)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            SpeechOptions.Locale = locale;
            SpeechOptions.Pitch = pitch;
            SpeechOptions.Volume = volume;
            this.IsPlaying = true;
            await TextToSpeech.SpeakAsync(text, SpeechOptions, CancellationTokenSource.Token);
            this.IsPlaying = false;
        }

        /// <summary>
        /// The Stop.
        /// </summary>
        internal void Stop()
        {
            this.IsPlaying = false;
            if (CancellationTokenSource == null || CancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            CancellationTokenSource.Cancel();
        }

        #endregion Methods
    }
}