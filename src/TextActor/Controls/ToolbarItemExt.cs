namespace TextActor.Controls
{
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="ToolbarItemExt" />.
    /// </summary>
    public class ToolbarItemExt : ToolbarItem
    {
        #region Fields

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ToolbarItemExt), true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsVisible.
        /// </summary>
        public bool IsVisible { get => (bool)GetValue(IsVisibleProperty); set => SetValue(IsVisibleProperty, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnIsVisibleChanged.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="BindableObject"/>.</param>
        /// <param name="oldvalue">The oldvalue<see cref="object"/>.</param>
        /// <param name="newvalue">The newvalue<see cref="object"/>.</param>
        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is ToolbarItemExt item) || item.Parent == null)
            {
                return;
            }

            var toolbarItems = ((ContentPage)item.Parent).ToolbarItems;
            var isVisible = (bool)newvalue;
            if (isVisible && !toolbarItems.Contains(item))
            {
                Device.BeginInvokeOnMainThread(() => { toolbarItems.Add(item); });
            }
            else if (!isVisible && toolbarItems.Contains(item))
            {
                Device.BeginInvokeOnMainThread(() => { toolbarItems.Remove(item); });
            }
        }

        #endregion Methods
    }
}