namespace TextActor.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using TextActor.Extensions;
    using TextActor.Helpers;
    using TextActor.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="StoryManagerViewModel" />.
    /// </summary>
    public class StoryManagerViewModel : BaseViewModel, IVisibilityChangedNotifiable
    {
        #region Fields

        private StoryViewModel _selectedItem;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryManagerViewModel"/> class.
        /// </summary>
        public StoryManagerViewModel()
        {
            Title = "Story Manager";
            Stories = new ObservableCollection<StoryViewModel>();
            AddCommand = new Command(OnAddAsync);
            EditSelectedCommand = new Command<StoryViewModel>(OnEditSelectedAsync);
            ItemTapped = new Command<StoryViewModel>(OnSelected);
            LoadCommand = new Command(async () => await OnExecuteLoad());
            PlaySelectedCommand = new Command<StoryViewModel>(OnPlaySelected);
            RemoveSelectedCommand = new Command<StoryViewModel>(OnRemoveSelected);
            TextPlayer = new TextPlayer();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddCommand.
        /// </summary>
        public Command AddCommand { get; }

        /// <summary>
        /// Gets the EditSelectedCommand.
        /// </summary>
        public Command EditSelectedCommand { get; }

        /// <summary>
        /// Gets the ItemTapped.
        /// </summary>
        public Command<StoryViewModel> ItemTapped { get; }

        /// <summary>
        /// Gets the LoadCommand.
        /// </summary>
        public Command LoadCommand { get; }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public Command PlayCommand { get; }

        /// <summary>
        /// Gets the PlaySelectedCommand.
        /// </summary>
        public Command<StoryViewModel> PlaySelectedCommand { get; }

        /// <summary>
        /// Gets the RemoveSelectedCommand.
        /// </summary>
        public Command<StoryViewModel> RemoveSelectedCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedItem.
        /// </summary>
        public StoryViewModel SelectedItem { get => _selectedItem; set => SetProperty(ref _selectedItem, value); }

        /// <summary>
        /// Gets the Stories.
        /// </summary>
        public ObservableCollection<StoryViewModel> Stories { get; }

        /// <summary>
        /// Gets the TextPlayer.
        /// </summary>
        public TextPlayer TextPlayer { get; }

        /// <summary>
        /// Gets or sets a value indicating whether IsPlaying.
        /// </summary>
        private bool IsPlaying { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnAppearing(object obj)
        {
            IsBusy = true;
            SelectedItem = null;
        }

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public async void OnDisappearing(object obj)
        {
            if (SelectedItem == null)
            {
                return;
            }

            await App.Database.UpdateSettingsAsync(settings => settings.EditedStoryId = SelectedItem.Id);
        }

        /// <summary>
        /// The OnAddAsync.
        /// </summary>
        /// <param name="story">The story<see cref="object"/>.</param>
        private async void OnAddAsync(object story) => await Shell.Current.GoToAsync(nameof(StoryPage));

        /// <summary>
        /// The OnEditSelectedAsync.
        /// </summary>
        /// <param name="story">The story<see cref="StoryViewModel"/>.</param>
        private async void OnEditSelectedAsync(StoryViewModel story)
        {
            SelectedItem = story;
            if (SelectedItem == null)
            {
                return;
            }

            // This will push the ActorDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(StoryPage)}?{nameof(StoryViewModel.Id)}={SelectedItem.Id}");
        }

        /// <summary>
        /// The OnExecuteLoad.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnExecuteLoad()
        {
            IsBusy = true;

            try
            {
                Stories.Clear();
                var stories = await StoryDataStore.GetItemsAsync(true);
                var settings = await App.Database.GetSettingsAsync();

                foreach (var story in stories)
                {
                    var item = new StoryViewModel { Id = story.Id };
                    Stories.Add(item);
                    if (item.Id == settings.Id)
                    {
                        SelectedItem = item;
                    }
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
        /// <param name="story">The story<see cref="StoryViewModel"/>.</param>
        private async void OnPlaySelected(StoryViewModel story)
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                return;
            }

            if (story == null)
            {
                return;
            }

            IsPlaying = true;
            await Task.Run(async () =>
                {
                    foreach (var dialog in story.DialogDetails)
                    {
                        if (!IsPlaying)
                        {
                            return;
                        }

                        var message = dialog.Message;
                        var actor = dialog.SelectedActor;
                        if (string.IsNullOrWhiteSpace(message) || actor == null)
                        {
                            continue;
                        }

                        await TextPlayer.Play(message, actor);
                    }

                    IsPlaying = false;
                });
        }

        /// <summary>
        /// The OnRemoveSelected.
        /// </summary>
        /// <param name="story">The story<see cref="StoryViewModel"/>.</param>
        private async void OnRemoveSelected(StoryViewModel story)
        {
            if (story == null || Stories == null || !Stories.Contains(story))
            {
                return;
            }

            this.IsBusy = true;
            await StoryDataStore.DeleteItemAsync(story.Id);
            this.IsBusy = false;
            await OnExecuteLoad();
        }

        /// <summary>
        /// The OnSelected.
        /// </summary>
        /// <param name="item">The item<see cref="StoryViewModel"/>.</param>
        private async void OnSelected(StoryViewModel item)
        {
            SelectedItem = item;
            await Stories.UnselectAll();

            if (SelectedItem == null)
            {
                return;
            }

            SelectedItem.IsSelected = true;
            await Stories.UnselectAll();

            if (SelectedItem == null)
            {
                return;
            }

            SelectedItem.IsSelected = true; ;
        }

        #endregion Methods
    }
}