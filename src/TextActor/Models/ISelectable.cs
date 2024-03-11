namespace TextActor.Models
{
    using System.ComponentModel;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="ISelectable" />.
    /// </summary>
    public interface ISelectable : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected.
        /// </summary>
        bool IsSelected { get; set; }

        #endregion Properties
    }

    #endregion Interfaces
}