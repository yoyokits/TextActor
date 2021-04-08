namespace TextActor.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IDataStore{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IDataStore<T>
    {
        #region Methods

        /// <summary>
        /// The AddItemAsync.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> AddItemAsync(T item);

        /// <summary>
        /// The DeleteItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> DeleteItemAsync(int id);

        /// <summary>
        /// The GetItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> GetItemAsync(int id);

        /// <summary>
        /// The GetItemsAsync.
        /// </summary>
        /// <param name="forceRefresh">The forceRefresh<see cref="bool"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{T}}"/>.</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        /// <summary>
        /// The UpdateItemAsync.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> UpdateItemAsync(T item);

        #endregion Methods
    }

    #endregion Interfaces
}