namespace TextActor.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using TextActor.Services;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="DialogDetailViewModel" />.
    /// </summary>
    public class DialogDetailViewModel : BaseViewModel, ISelectable
    {
        #region Fields

        private IEnumerable<Actor> _actors;

        private bool _isSelected;

        private string _message;

        private Actor _selectedActor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogDetailViewModel"/> class.
        /// </summary>
        public DialogDetailViewModel()
        {
            SelectedActor = TextActorDataBase.DefaultActor;
            PlayCommand = new Command(OnPlay, Validate);
            StopCommand = new Command(this.OnStop);
            TextPlayer = new TextPlayer();
            this.PropertyChanged +=
                (_, e) =>
                {
                    if (e.PropertyName == nameof(this.Message))
                    {
                        PlayCommand.ChangeCanExecute();
                    }
                };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Actors.
        /// </summary>
        public IEnumerable<Actor> Actors { get => _actors; internal set => this.SetProperty(ref _actors, value); }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; } = IdCounter++;

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected.
        /// </summary>
        public bool IsSelected { get => _isSelected; set => this.SetProperty(ref _isSelected, value); }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get => _message; set => this.SetProperty(ref _message, value); }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public Command PlayCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedActor.
        /// </summary>
        public Actor SelectedActor { get => _selectedActor; internal set => this.SetProperty(ref _selectedActor, value); }

        /// <summary>
        /// Gets the StopCommand.
        /// </summary>
        public ICommand StopCommand { get; }

        /// <summary>
        /// Gets the TextPlayer.
        /// </summary>
        public TextPlayer TextPlayer { get; }

        /// <summary>
        /// Gets or sets the IdCounter.
        /// </summary>
        private static int IdCounter { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"{SelectedActor.Name}:{Message.Substring(0, 20)}";
            return message;
        }

        /// <summary>
        /// The ValidateDialog.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ValidateDialog()
        {
            var isValid = SelectedActor != null && !string.IsNullOrWhiteSpace(Message);
            return isValid;
        }

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnPlay(object obj)
        {
            await TextPlayer.Play(this.Message, this.SelectedActor);
        }

        /// <summary>
        /// The OnStop.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnStop(object obj)
        {
            TextPlayer.Stop();
        }

        /// <summary>
        /// The Validate.
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