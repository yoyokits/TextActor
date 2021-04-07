namespace TextActor.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextActor.Models;

    /// <summary>
    /// Defines the <see cref="ActorDataStore" />.
    /// </summary>
    public class ActorDataStore : IDataStore<Actor>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorDataStore"/> class.
        /// </summary>
        public ActorDataStore()
        {
            Actors = new List<Actor>()
            {
                new Actor { Id="0", Name = "Ivana", Pitch=0.8f, Volume=0.6f, LocaleName="English(United States)" },
                new Actor { Id="1",Name = "Olga", Pitch=0.7f, Volume=0.8f, LocaleName="Russian (Russia)" },
                new Actor { Id="2",Name = "Indira", Pitch=0.7f, Volume=0.8f , LocaleName="Hindi (India)"},
            };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Actors.
        /// </summary>
        private List<Actor> Actors { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The AddItemAsync.
        /// </summary>
        /// <param name="item">The item<see cref="Actor"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> AddItemAsync(Actor item)
        {
            Actors.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// The DeleteItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = Actors.Where((Actor arg) => arg.Id == id).FirstOrDefault();
            Actors.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// The GetItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{Actor}"/>.</returns>
        public async Task<Actor> GetItemAsync(string id)
        {
            return await Task.FromResult(Actors.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// The GetItemsAsync.
        /// </summary>
        /// <param name="forceRefresh">The forceRefresh<see cref="bool"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Actor}}"/>.</returns>
        public async Task<IEnumerable<Actor>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Actors);
        }

        /// <summary>
        /// The UpdateItemAsync.
        /// </summary>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> UpdateItemAsync(Actor actor)
        {
            var oldItem = Actors.Where((Actor arg) => arg.Id == actor.Id).FirstOrDefault();
            Actors.Remove(oldItem);
            Actors.Add(actor);

            return await Task.FromResult(true);
        }

        #endregion Methods
    }
}