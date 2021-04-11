namespace TextActor.Views
{
    /// <summary>
    /// Defines the <see cref="NewStoryPage" />.
    /// </summary>
    public class NewStoryPage : StoryPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewStoryPage"/> class.
        /// </summary>
        public NewStoryPage()
        {
            ViewModel.IsNewItemMode = true;
        }

        #endregion Constructors
    }
}