namespace TextActor.ViewModels
{
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="AboutViewModel" />.
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/yoyokits/TextActor"));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the OpenWebCommand.
        /// </summary>
        public ICommand OpenWebCommand { get; }

        #endregion Properties
    }
}