namespace TextActor.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextActor.Models;

    /// <summary>
    /// Defines the <see cref="StoryDataStore" />.
    /// </summary>
    public class StoryDataStore : IDataStore<Story>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryDataStore"/> class.
        /// </summary>
        public StoryDataStore()
        {
            Stories = new List<Story>()
            {
                new Story { Id=0, Title = "Meet Friend on Beach", Date=DateTime.Now, DialogsJson = string.Empty},
                new Story { Id=0, Title = "Birthday Party", Date=DateTime.Now, DialogsJson = string.Empty}
            };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Stories.
        /// </summary>
        private List<Story> Stories { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The AddItemAsync.
        /// </summary>
        /// <param name="item">The item<see cref="Story"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> AddItemAsync(Story item)
        {
            Stories.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// The DeleteItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Stories.Where((Story arg) => arg.Id == id).FirstOrDefault();
            Stories.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// The GetItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{Story}"/>.</returns>
        public async Task<Story> GetItemAsync(int id)
        {
            return await Task.FromResult(Stories.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// The GetItemsAsync.
        /// </summary>
        /// <param name="forceRefresh">The forceRefresh<see cref="bool"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Story}}"/>.</returns>
        public async Task<IEnumerable<Story>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Stories);
        }

        /// <summary>
        /// The UpdateItemAsync.
        /// </summary>
        /// <param name="story">The actor<see cref="Story"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> UpdateItemAsync(Story story)
        {
            var oldItem = Stories.Where((Story arg) => arg.Id == story.Id).FirstOrDefault();
            Stories.Remove(oldItem);
            Stories.Add(story);

            return await Task.FromResult(true);
        }

        #endregion Methods
    }
}