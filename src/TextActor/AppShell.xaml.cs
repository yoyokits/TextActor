namespace TextActor
{
    using TextActor.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="AppShell" />.
    /// </summary>
    public partial class AppShell : Xamarin.Forms.Shell
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppShell"/> class.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ActorDetailPage), typeof(ActorDetailPage));
            Routing.RegisterRoute(nameof(NewActorPage), typeof(NewActorPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        #endregion Constructors
    }
}