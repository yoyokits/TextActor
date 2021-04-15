namespace TextActor.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using TextActor.Helpers;
    using TextActor.Models;
    using TextActor.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="ActorsViewModel" />.
    /// </summary>
    public class ActorsViewModel : BaseViewModel, IVisibilityChangedNotifiable
    {
        #region Fields

        private Actor _selectedActor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorsViewModel"/> class.
        /// </summary>
        public ActorsViewModel()
        {
            Title = "Actors";
            Actors = new ObservableCollection<Actor>();
            ActorTapped = new Command<Actor>(OnActorSelected);
            AddActorCommand = new Command(OnAddActor);
            LoadActorsCommand = new Command(async () => await OnExecuteLoadActors());
            PlaySelectedCommand = new Command(OnPlaySelected);
            RemoveSelectedCommand = new Command<Actor>(OnRemoveSelected);
            TextPlayer = new TextPlayer();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Actors.
        /// </summary>
        public ObservableCollection<Actor> Actors { get; }

        /// <summary>
        /// Gets the ActorTapped.
        /// </summary>
        public Command<Actor> ActorTapped { get; }

        /// <summary>
        /// Gets the AddActorCommand.
        /// </summary>
        public ICommand AddActorCommand { get; }

        /// <summary>
        /// Gets the LoadActorsCommand.
        /// </summary>
        public ICommand LoadActorsCommand { get; }

        /// <summary>
        /// Gets the PlaySelectedCommand.
        /// </summary>
        public ICommand PlaySelectedCommand { get; }

        /// <summary>
        /// Gets the RemoveSelectedCommand.
        /// </summary>
        public Command<Actor> RemoveSelectedCommand { get; }

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
        /// The OnAppearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnAppearing(object obj)
        {
            IsBusy = true;
            SelectedActor = null;
        }

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public async void OnDisappearing(object obj)
        {
            await Task.Run(async () =>
            {
                var actorList = Actors.ToList();
                foreach (var actor in actorList)
                {
                    await App.Database.SaveActorAsync(actor);
                }
            });
        }

        /// <summary>
        /// The OnActorSelected.
        /// </summary>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        private async void OnActorSelected(Actor actor)
        {
            SelectedActor = actor;
            if (actor == null)
            {
                return;
            }

            // This will push the ActorDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ActorPage)}?{nameof(ActorViewModel.Id)}={actor.Id}");
        }

        /// <summary>
        /// The OnAddActor.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnAddActor(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ActorPage));
        }

        /// <summary>
        /// The OnExecuteLoadActors.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnExecuteLoadActors()
        {
            IsBusy = true;

            try
            {
                Actors.Clear();
                var items = await App.Database.GetActorsAsync();
                foreach (var item in items)
                {
                    Actors.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// The OnPlaySelected.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnPlaySelected(object obj)
        {
            if (SelectedActor == null)
            {
                return;
            }

            await TextPlayer.Play(TextPlayer.DefaultTestText, SelectedActor);
        }

        /// <summary>
        /// The OnRemoveSelected.
        /// </summary>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        private async void OnRemoveSelected(Actor actor)
        {
            if (actor == null || Actors == null || !Actors.Contains(actor))
            {
                return;
            }

            this.IsBusy = true;
            await App.Database.DeleteActorAsync(actor);
            this.IsBusy = false;
        }

        #endregion Methods
    }
}