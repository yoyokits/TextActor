namespace TextActor.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using TextActor.Converters;
    using TextActor.Extensions;
    using TextActor.Helpers;
    using TextActor.Models;
    using TextActor.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="StoryViewModel" />.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public class StoryViewModel : BaseViewModel, ISelectable
    {
        #region Fields

        private bool _isPlaying;

        private bool _isSelected;

        private string _playButtonImage = "media_play.png";

        private DialogDetailViewModel _selectedDialog;

        private string _storyTitle;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryViewModel"/> class.
        /// </summary>
        public StoryViewModel()
        {
            Title = "Story";
            DialogDetails = new ObservableCollection<DialogDetailViewModel>();
            DialogTapped = new Command<DialogDetailViewModel>(OnDialogSelected);
            AddDialogCommand = new Command(OnAddDialog);
            ClearDialogCommand = new Command(OnClearDialogs);
            LoadDialogsCommand = new Command(async () => await OnExecuteLoadDialogs());
            NewStoryCommand = new Command(OnNewStory);
            OpenStoryManagerCommand = new Command(OnOpenStoryManager);
            PlayCommand = new Command(OnPlay);
            PlaySelectedCommand = new Command<DialogDetailViewModel>(OnPlaySelected);
            RemoveSelectedCommand = new Command<DialogDetailViewModel>(OnRemoveSelected);
            TextPlayer = new TextPlayer();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddDialogCommand.
        /// </summary>
        public ICommand AddDialogCommand { get; }

        /// <summary>
        /// Gets the ClearDialogCommand.
        /// </summary>
        public ICommand ClearDialogCommand { get; }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the DialogDetails.
        /// </summary>
        public ObservableCollection<DialogDetailViewModel> DialogDetails { get; }

        /// <summary>
        /// Gets the DialogTapped.
        /// </summary>
        public Command<DialogDetailViewModel> DialogTapped { get; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsNewItemMode.
        /// </summary>
        public bool IsNewItemMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected.
        /// </summary>
        public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }

        /// <summary>
        /// Gets the LoadDialogsCommand.
        /// </summary>
        public ICommand LoadDialogsCommand { get; }

        /// <summary>
        /// Gets the NewStoryCommand.
        /// </summary>
        public ICommand NewStoryCommand { get; }

        /// <summary>
        /// Gets the OpenStoryManagerCommand.
        /// </summary>
        public ICommand OpenStoryManagerCommand { get; }

        /// <summary>
        /// Gets the PlayButtonImage.
        /// </summary>
        public string PlayButtonImage { get => _playButtonImage; private set => this.SetProperty(ref _playButtonImage, value); }

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public ICommand PlayCommand { get; }

        /// <summary>
        /// Gets the PlaySelectedCommand.
        /// </summary>
        public Command<DialogDetailViewModel> PlaySelectedCommand { get; }

        /// <summary>
        /// Gets the RemoveSelectedCommand.
        /// </summary>
        public Command<DialogDetailViewModel> RemoveSelectedCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedDialog.
        /// </summary>
        public DialogDetailViewModel SelectedDialog { get => _selectedDialog; set => this.SetProperty(ref _selectedDialog, value); }

        /// <summary>
        /// Gets or sets the StoryTitle.
        /// </summary>
        public string StoryTitle { get => _storyTitle; set => this.SetProperty(ref _storyTitle, value); }

        /// <summary>
        /// Gets the TextPlayer.
        /// </summary>
        public TextPlayer TextPlayer { get; }

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
        /// The OnAppearing.
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedDialog = null;
        }

        /// <summary>
        /// The OnAddDialog.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnAddDialog(object obj) => await Shell.Current.GoToAsync(nameof(NewDialogPage));

        /// <summary>
        /// The OnClearDialogs.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnClearDialogs(object obj)
        {
            var page = obj as Page;
            var isYes = await page.DisplayAlert("Clear Story?", "Would you like to delete all dialogs?", "Yes", "No");
            if (!isYes)
            {
                return;
            }

            this.IsBusy = true;
            foreach (var dialog in DialogDetails)
            {
                await this.DialogDataStore.DeleteItemAsync(dialog.Id);
            }

            this.IsBusy = false;
            await OnExecuteLoadDialogs();
        }

        /// <summary>
        /// The OnDialogSelected.
        /// </summary>
        /// <param name="dialogDetail">The obj<see cref="Dialog"/>.</param>
        private async void OnDialogSelected(DialogDetailViewModel dialogDetail)
        {
            SelectedDialog = dialogDetail;
            await DialogDetails.UnselectAll();

            if (SelectedDialog == null)
            {
                return;
            }

            SelectedDialog.IsSelected = true; ;
        }

        /// <summary>
        /// The OnExecuteLoadDialogs.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnExecuteLoadDialogs()
        {
            IsBusy = true;

            try
            {
                DialogDetails.Clear();
                var items = await DialogDataStore.GetItemsAsync(true);
                var actors = await ActorDataStore.GetItemsAsync();
                if (actors == null || !actors.Any())
                {
                    return;
                }

                foreach (var item in items)
                {
                    var selectedActor = actors.Where(actor => actor.Id == item.ActorId).FirstOrDefault();
                    var detail = new DialogDetailViewModel { Actors = actors, SelectedActor = selectedActor };
                    detail.LoadDialogId(item.Id);

                    DialogDetails.Add(detail);
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
        /// The OnNewStory.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnNewStory(object obj)
        {
            var isStoryValid = await ValidateStory();
            if (!isStoryValid)
            {
                return;
            }

            var story = await this.ToStory();
            await StoryDataStore.AddItemAsync(story);
        }

        /// <summary>
        /// The OnOpenStoryManager.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnOpenStoryManager(object obj) => await Shell.Current.GoToAsync(nameof(StoryManagerPage));

        /// <summary>
        /// The OnPlay.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPlay(object obj) => this.Play(0);

        /// <summary>
        /// The OnPlaySelected.
        /// </summary>
        /// <param name="dialog">The dialog<see cref="DialogDetailViewModel"/>.</param>
        private void OnPlaySelected(DialogDetailViewModel dialog)
        {
            if (dialog == null || DialogDetails == null || !DialogDetails.Contains(dialog))
            {
                return;
            }

            var startIndex = DialogDetails.IndexOf(dialog);
            Play(startIndex);
        }

        /// <summary>
        /// The OnRemoveSelected.
        /// </summary>
        /// <param name="dialog">The dialog<see cref="DialogDetailViewModel"/>.</param>
        private async void OnRemoveSelected(DialogDetailViewModel dialog)
        {
            if (dialog == null || DialogDetails == null || !DialogDetails.Contains(dialog))
            {
                return;
            }

            this.IsBusy = true;
            await DialogDataStore.DeleteItemAsync(dialog.Id);
            this.IsBusy = false;
            await OnExecuteLoadDialogs();
        }

        /// <summary>
        /// The Play.
        /// </summary>
        /// <param name="startIndex">The startIndex<see cref="int"/>.</param>
        private void Play(int startIndex)
        {
            if (this.IsPlaying)
            {
                this.CancellationTokenSource?.Cancel();
                this.IsPlaying = false;
                TextPlayer.Stop();
                return;
            }

            this.IsPlaying = true;
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;
            Task.Run(async () =>
            {
                if (this.DialogDetails == null || !this.DialogDetails.Any())
                {
                    this.IsPlaying = false;
                    return;
                }

                var dialogsCopy = this.DialogDetails.ToList();
                var index = startIndex;

                while (this.IsPlaying && !token.IsCancellationRequested && index < dialogsCopy.Count)
                {
                    var dialog = dialogsCopy[index++];
                    await DialogDetails.UnselectAll();
                    dialog.IsSelected = true;
                    await TextPlayer.Play(dialog.Message, dialog.SelectedActor);
                }

                this.IsPlaying = false;
            }, token);
        }

        /// <summary>
        /// The ValidateStory.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private async Task<bool> ValidateStory()
        {
            if (string.IsNullOrWhiteSpace(this.StoryTitle))
            {
                return false;
            }

            var isValid = true;
            await Task.Run(() =>
            {
                foreach (var dialog in DialogDetails)
                {
                    if (!dialog.ValidateDialog())
                    {
                        isValid = false;
                        return;
                    }
                }
            });

            return isValid;
        }

        #endregion Methods
    }
}