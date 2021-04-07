namespace TextActor.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="NewDialogViewModel" />.
    /// </summary>
    public class NewDialogViewModel : BaseViewModel
    {
        #region Fields

        private List<Actor> _actors;

        private string _message;

        private Actor _selectedActor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewDialogViewModel"/> class.
        /// </summary>
        public NewDialogViewModel()
        {
            PlayCommand = new Command(OnPlay, Validate);
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave, Validate);
            TextPlayer = new TextPlayer();
            this.PropertyChanged +=
                (_, __) =>
                {
                    PlayCommand.ChangeCanExecute();
                    SaveCommand.ChangeCanExecute();
                };
            Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Actors.
        /// </summary>
        public List<Actor> Actors { get => _actors; private set => this.SetProperty(ref _actors, value); }

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get => _message; set => this.SetProperty(ref _message, value); }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public Command PlayCommand { get; }

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public Command SaveCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedActor.
        /// </summary>
        public Actor SelectedActor { get => _selectedActor; set => this.SetProperty(ref _selectedActor, value); }

        /// <summary>
        /// Gets the TextPlayer.
        /// </summary>
        public TextPlayer TextPlayer { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Initialize.
        /// </summary>
        private async void Initialize()
        {
            var actors = await ActorDataStore.GetItemsAsync();
            if (actors == null || !actors.Any())
            {
                return;
            }

            Actors = actors.ToList();
            SelectedActor = actors.First();
        }

        /// <summary>
        /// The OnCancel.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnCancel(object obj) => await Shell.Current.GoToAsync("..");

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnPlay(object obj) => await TextPlayer.Play(Message, SelectedActor);

        /// <summary>
        /// The OnSave.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnSave(object obj)
        {
            var dialog = new Dialog()
            {
                ActorId = SelectedActor.Id,
                Message = Message
            };

            await DialogDataStore.AddItemAsync(dialog);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// The Validate Save and Play.
        /// </summary>
        /// <param name="arg">The arg<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool Validate(object arg)
        {
            return !string.IsNullOrEmpty(Message) && !string.IsNullOrWhiteSpace(Message) && SelectedActor != null;
        }

        #endregion Methods
    }
}