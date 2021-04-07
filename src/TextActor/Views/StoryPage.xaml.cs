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
        #region Fields

        private StoryViewModel _viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryPage"/> class.
        /// </summary>
        public StoryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new StoryViewModel();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion Methods
    }
}