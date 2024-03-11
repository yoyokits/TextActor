namespace TextActor.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="INotifyPropertyChangedExtension" />.
    /// </summary>
    public static class INotifyPropertyChangedExtension
    {
        #region Delegates

        /// <summary>
        /// The PropertyChangedInvokerEventHandler
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/></param>
        public delegate void PropertyChangedInvokerEventHandler(string propertyName);

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Invokes the properties changed.
        /// </summary>
        /// <param name="instance">The INotifyPropertyChanged implementation.</param>
        /// <param name="invoker">The invoker.</param>
        /// <param name="propertyNames">The property name list.</param>
        public static void InvokePropertiesChanged(this INotifyPropertyChanged instance, PropertyChangedInvokerEventHandler invoker, params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                invoker?.Invoke(propertyName);
            }
        }

        /// <summary>
        /// The IsMatch.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/>.</param>
        /// <param name="propertyNames">The <see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsMatch(this PropertyChangedEventArgs e, params string[] propertyNames)
        {
            var propertyName = e.PropertyName;
            foreach (var name in propertyNames)
            {
                if (propertyName == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the specified invoker.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="invoker">The invoker.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyNames">The other optional property names to notify.</param>
        /// <returns> True if old value and new value are different. .</returns>
        public static bool Set<T>(this INotifyPropertyChanged instance, PropertyChangedInvokerEventHandler invoker, ref T field, T value, [CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            invoker?.Invoke(propertyName);
            if (propertyNames != null && invoker != null)
            {
                foreach (var name in propertyNames)
                {
                    invoker.Invoke(name);
                }
            }

            return true;
        }

        /// <summary>
        /// The Set.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyChanged">The property changed.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns> True if old value and new value are different. .</returns>
        public static bool Set<T>(this INotifyPropertyChanged sender, PropertyChangedEventHandler propertyChanged, ref T field, T value, [CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            if (propertyNames != null && propertyChanged != null)
            {
                foreach (var name in propertyNames)
                {
                    propertyChanged.Invoke(sender, new PropertyChangedEventArgs(name));
                }
            }

            return true;
        }

        #endregion Methods
    }
}