// ========================================== //
// Developer: Yohanes Wahyu Nurcahyo          //
// Website: https://github.com/yoyokits       //
// ========================================== //

namespace TextActor.ViewModels
{
    using System;
    using System.Collections.Generic;
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
    using TextActor.Services;
    using TextActor.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="StoryViewModel" />.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))]
    public class StoryViewModel : BaseViewModel, ISelectable, IVisibilityChangedNotifiable
    {
        #region Fields

        private int _id;

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
            CancelNewStoryCommand = new Command(OnCancelNewStory);
            ClearDialogCommand = new Command(OnClearDialogs);
            LoadDialogsCommand = new Command(async () => await OnExecuteLoadDialogs());
            NewStoryCommand = new Command(OnNewStory);
            OpenStoryManagerCommand = new Command(OnOpenStoryManager);
            PlayCommand = new Command(OnPlay);
            PlaySelectedCommand = new Command<DialogDetailViewModel>(OnPlaySelected);
            RemoveSelectedCommand = new Command<DialogDetailViewModel>(OnRemoveSelected);
            TextPlayer = new TextPlayer();
            Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddDialogCommand.
        /// </summary>
        public ICommand AddDialogCommand { get; }

        /// <summary>
        /// Gets the CancelNewStoryCommand.
        /// </summary>
        public Command CancelNewStoryCommand { get; }

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
                this.OnExecuteLoadDialogs();
            }
        }

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
        /// Gets or sets a value indicating whether IsCancelNewStory.
        /// </summary>
        private bool IsCancelNewStory { get; set; }

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
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnAppearing(object obj)
        {
            IsBusy = true;
            IsCancelNewStory = false;
            SelectedDialog = null;
        }

        /// <summary>
        /// The OnDisappearing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public void OnDisappearing(object obj)
        {
            if (IsCancelNewStory)
            {
                return;
            }

            SaveStory();
        }

        public IEnumerable<string> Sort(List<string> items)
        {
            return items.OrderBy(item => item);
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private async void Initialize()
        {
            var settings = await App.Database.GetSettingsAsync();
            this.Id = settings.EditedStoryId;
        }

        /// <summary>
        /// The OnAddDialog.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private async void OnAddDialog(object obj)
        {
            var actors = DialogDetails.Any() ? DialogDetails.First().Actors : await App.Database.GetActorsAsync();
            var defaultActor = TextActorDataBase.DefaultActor;
            var dialog = new DialogDetailViewModel { Actors = actors, SelectedActor = defaultActor };
            DialogDetails.Add(dialog);
        }

        /// <summary>
        /// The OnCancelNewStory.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnCancelNewStory(object obj)
        {
            IsCancelNewStory = true;
        }

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
            this.DialogDetails.Clear();
            this.IsBusy = false;
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
                var actors = await App.Database.GetActorsAsync();
                if (actors == null || !actors.Any())
                {
                    return;
                }

                var story = await App.Database.GetStoryAsync(this.Id);
                if (story == null)
                {
                    return;
                }

                StoryTitle = story.Name;
                var dialogs = await story.GetDialogs(actors);
                foreach (var dialog in dialogs)
                {
                    var selectedActor = actors.Where(actor => actor.Id == dialog.ActorId).FirstOrDefault();
                    var detail = new DialogDetailViewModel { Actors = actors, Message = dialog.Message, SelectedActor = selectedActor };
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
        private void OnRemoveSelected(DialogDetailViewModel dialog)
        {
            if (dialog == null || DialogDetails == null || !DialogDetails.Contains(dialog))
            {
                return;
            }

            DialogDetails.Remove(dialog);
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
        /// The SaveStory.
        /// </summary>
        private async void SaveStory()
        {
            var story = await this.ToStory();
            await App.Database.SaveStoryAsync(story);
            await App.Database.UpdateSettingsAsync(settings => settings.EditedStoryId = story.Id);
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