namespace TextActor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="DialogDetailViewModel" />.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public class DialogDetailViewModel : BaseViewModel
    {
        #region Fields

        private IEnumerable<Actor> _actors;

        private int _id;

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
            TextPlayer = new TextPlayer();
            PlayCommand = new Command(this.OnPlay);
            StopCommand = new Command(this.OnStop);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ActorId.
        /// </summary>
        public int ActorId { get; private set; }

        /// <summary>
        /// Gets or sets the Actors.
        /// </summary>
        public IEnumerable<Actor> Actors { get => _actors; internal set => this.SetProperty(ref _actors, value); }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                LoadDialogId(Id);
                LoadActors();
            }
        }

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
        public ICommand PlayCommand { get; }

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// The LoadDialogId.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        internal async void LoadDialogId(int id)
        {
            try
            {
                var dialog = await DialogDataStore.GetItemAsync(id);
                _id = id;
                ActorId = dialog.ActorId;
                Message = dialog.Message;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        /// <summary>
        /// The LoadActors.
        /// </summary>
        private async void LoadActors()
        {
            Actors = await ActorDataStore.GetItemsAsync();
            SelectedActor = Actors.Where(actor => actor.Id == ActorId).FirstOrDefault();
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

        #endregion Methods
    }
}