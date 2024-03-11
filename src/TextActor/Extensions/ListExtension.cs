namespace TextActor.Extensions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextActor.Models;

    /// <summary>
    /// Defines the <see cref="ListExtension" />.
    /// </summary>
    public static class ListExtension
    {
        #region Methods

        /// <summary>
        /// The ToIdDictionary.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="items">The items<see cref="IEnumerable{T}"/>.</param>
        /// <returns>The <see cref="IDictionary{int, T}"/>.</returns>
        public static IDictionary<int, T> ToIdDictionary<T>(this IEnumerable<T> items) where T : IIdentifiable
        {
            var idDict = new Dictionary<int, T>();
            foreach (var item in items)
            {
                idDict[item.Id] = item;
            }

            return idDict;
        }

        /// <summary>
        /// The UnselectAll.
        /// </summary>
        /// <param name="selectableItems">The selectableItems<see cref="IEnumerable{ISelectable}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task UnselectAll(this IEnumerable<ISelectable> selectableItems)
        {
            if (selectableItems == null)
            {
                return;
            }

            await Task.Run(() =>
            {
                foreach (var item in selectableItems)
                {
                    item.IsSelected = false;
                }
            });
        }

        #endregion Methods
    }
}