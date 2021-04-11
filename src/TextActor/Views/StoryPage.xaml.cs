﻿namespace TextActor.Views
{
    using TextActor.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Defines the <see cref="StoryPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoryPage : ContentPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryPage"/> class.
        /// </summary>
        public StoryPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new StoryViewModel();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ViewModel.
        /// </summary>
        protected StoryViewModel ViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        #endregion Methods
    }
}