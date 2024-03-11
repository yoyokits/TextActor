namespace TextActor.Extensions
{
    using System;
    using TextActor.ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="VisibilityChangeExtension" />.
    /// </summary>
    public static class VisibilityChangeExtension
    {
        #region Fields

        public static readonly BindableProperty NotifyVisibilityChangedProperty = BindableProperty.CreateAttached("NotifyVisibilityChanged", typeof(bool), typeof(VisibilityChangeExtension), false, BindingMode.OneWay, null, OnNotifyVisibilityChangedChanged);

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetHNotifyVisibilityChanged.
        /// </summary>
        /// <param name="view">The view<see cref="BindableObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetNotifyVisibilityChanged(BindableObject view) => (bool)view.GetValue(NotifyVisibilityChangedProperty);

        /// <summary>
        /// The SetNotifyVisibilityChanged.
        /// </summary>
        /// <param name="view">The view<see cref="BindableObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetNotifyVisibilityChanged(BindableObject view, bool value) => view.SetValue(NotifyVisibilityChangedProperty, value);

        /// <summary>
        /// The OnNotifyVisibilityChangedChanged.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="BindableObject"/>.</param>
        /// <param name="oldValue">The oldValue<see cref="object"/>.</param>
        /// <param name="newValue">The newValue<see cref="object"/>.</param>
        private static void OnNotifyVisibilityChangedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Page page))
            {
                return;
            }

            page.Appearing -= OnPage_Appearing;
            page.Disappearing -= OnPage_Disappearing; ;
            if ((bool)newValue)
            {
                page.Appearing += OnPage_Appearing;
                page.Disappearing += OnPage_Disappearing; ;
            }
        }

        /// <summary>
        /// The OnPage_Appearing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private static void OnPage_Appearing(object sender, EventArgs e)
        {
            if (!(((Page)sender).BindingContext is IVisibilityChangedNotifiable notifiable))
            {
                return;
            }

            notifiable.OnAppearing(sender);
        }

        /// <summary>
        /// The OnPage_Disappearing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private static void OnPage_Disappearing(object sender, EventArgs e)
        {
            if (!(((Page)sender).BindingContext is IVisibilityChangedNotifiable notifiable))
            {
                return;
            }

            notifiable.OnDisappearing(sender);
        }

        #endregion Methods
    }
}