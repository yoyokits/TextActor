namespace TextActor.Views
{
    using TextActor.ViewModels;

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
            ((StoryViewModel)BindingContext).IsNewItemMode = true;
        }

        #endregion Constructors
    }
}