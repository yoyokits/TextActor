namespace TextActor.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Input;
    using TextActor.Helpers;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="ActorDetailViewModel" />.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ActorDetailViewModel : BaseViewModel
    {
        #region Fields

        private string _id;

        private string _name;

        private float _pitch;

        private SpeechOptions _speechOptions;

        private float _volume;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorDetailViewModel"/> class.
        /// </summary>
        public ActorDetailViewModel()
        {
            PlayCommand = new Command(this.OnPlay);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the CancellationTokenSource.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id
        {
            get => _id;
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                LoadActorId(Id);
            }
        }

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

                _speechOptions = new SpeechOptions() { Locale = TextToSpeechHelper.DefaultLocale, Pitch = Pitch, Volume = Volume };
                return _speechOptions;
            }
        }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        public float Volume { get => _volume; set => this.SetProperty(ref _volume, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The LoadActorId.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        public async void LoadActorId(string id)
        {
            try
            {
                var actor = await ActorDataStore.GetItemAsync(id);
                Id = actor.Id;
                Name = actor.Name;
                Pitch = actor.Pitch;
                Volume = actor.Volume;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPlay(object obj)
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            SpeechOptions.Pitch = Pitch;
            SpeechOptions.Volume = Volume;
            TextToSpeech.SpeakAsync("This is Text Actor voice test.", SpeechOptions, CancellationTokenSource.Token);
        }

        #endregion Methods
    }
}